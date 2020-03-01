using AddressBook.Api.Infrastructure;
using AddressBook.Api.Infrastructure.ApiResponse;
using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using System;
using System.IO;
using System.Net;
using System.Threading.Tasks;

namespace AddressBook.Api.Middleware.ApiResponseWrapper
{
    public class ApiResponseProcessor
    {
        private readonly HttpContext context;
        private readonly RequestDelegate next;
        private readonly IApiResponseFactory apiResponseFactory;
        private Stream originalBodyStream;

        public ApiResponseProcessor(HttpContext context, RequestDelegate next, IApiResponseFactory apiResponseFactory)
        {
            this.context = context;
            this.next = next;
            this.apiResponseFactory = apiResponseFactory;
        }

        public async Task ProcessAsync()
        {
            originalBodyStream = context.Response.Body;

            var isWrappingNecessary = IsResponseWrappingNecessary();
            if (isWrappingNecessary)
            {
                try
                {
                    await WrapHttpResponseInApiResponse();
                }
                catch (Exception)
                {
                    RestoreOriginalHttpResponseBody();
                    throw;
                }
            }
            else
            {
                await next(context);
            }
        }

        private bool IsResponseWrappingNecessary()
        {
            var isSwaggerRequest = context.Request.Path.StartsWithSegments(new PathString("/swagger"));
            return !isSwaggerRequest;
        }

        private async Task WrapHttpResponseInApiResponse()
        {
            using (var memStream = new MemoryStream())
            {
                await ReadOriginalHttpResponseBodyIntoStream(memStream);
                RestoreOriginalHttpResponseBody();
                if (context.Response.StatusCode != (int)HttpStatusCode.NoContent)
                {
                    var bodyObject = DeserializeHttpResponseBodyFromStream(memStream);
                    var apiResponse = apiResponseFactory.CreateApiResponse(context.Response.StatusCode, bodyObject);
                    await WriteApiResponseToHttpResponse(apiResponse);
                }
            }
        }

        private async Task ReadOriginalHttpResponseBodyIntoStream(MemoryStream memStream)
        {
            context.Response.Body = memStream;
            await next(context);
        }

        private void RestoreOriginalHttpResponseBody()
        {
            context.Response.Body = originalBodyStream;
        }

        private object DeserializeHttpResponseBodyFromStream(MemoryStream memStream)
        {
            memStream.Seek(0, SeekOrigin.Begin);
            using (var streamReader = new StreamReader(memStream))
            {
                var readToEnd = streamReader.ReadToEnd();
                try
                {
                    var resultObject = JsonConvert.DeserializeObject(readToEnd);
                    return resultObject;
                }
                catch (JsonException)
                {
                    return readToEnd;
                }
            }             
        }

        private async Task WriteApiResponseToHttpResponse(ApiResponse apiResponse)
        {
            context.Response.ContentType = HttpResponseContentTypes.ApplicationJson;
            var serializationSettings = new JsonSerializerSettings()
            {
                ContractResolver = new DefaultContractResolver
                {
                    NamingStrategy = new CamelCaseNamingStrategy()
                }
            };
            var apiResultJson = JsonConvert.SerializeObject(apiResponse, serializationSettings);
            context.Response.ContentLength = apiResultJson.Length;
            await context.Response.WriteAsync(apiResultJson);
        }
    }
}
