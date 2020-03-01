using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using System;
using System.Threading.Tasks;

namespace AddressBook.Api.Middleware.ExceptionToHttpResponseMapper
{
    public class ExceptionToHttpResponseMapperMiddleware
    {
        private readonly RequestDelegate next;

        private readonly IExceptionToHttpResponseParametersMapper exceptionToHttpStatusCodeMapper;
        private readonly ILogger<ExceptionToHttpResponseMapperMiddleware> logger;

        public ExceptionToHttpResponseMapperMiddleware
        (
            RequestDelegate next,
            IExceptionToHttpResponseParametersMapper exceptionToHttpStatusCodeMapper,
            ILogger<ExceptionToHttpResponseMapperMiddleware> logger
        )
        {
            this.next = next;
            this.exceptionToHttpStatusCodeMapper = exceptionToHttpStatusCodeMapper;
            this.logger = logger;
        }

        public async Task InvokeAsync(HttpContext context)
        {
            try
            {
                await next(context);
            }
            catch (Exception exception)
            {
                logger.LogError(exception, exception.Message);
                if (exceptionToHttpStatusCodeMapper.TryGetHttpStatusCodeForException(exception, out HttpResponseParameters parameters))
                {
                    await MapExceptionToHttpResponse(context, parameters);
                }
                else
                {
                    throw;
                }
            }
        }

        private async Task MapExceptionToHttpResponse(HttpContext context, HttpResponseParameters parameters)
        {
            context.Response.StatusCode = parameters.StatusCode;
            context.Response.ContentType = parameters.ContentType;
            await context.Response.WriteAsync(parameters.Content);
        }
    }
}
