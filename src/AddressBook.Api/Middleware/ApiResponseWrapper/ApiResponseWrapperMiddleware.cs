using Microsoft.AspNetCore.Http;
using System.Threading.Tasks;

namespace AddressBook.Api.Middleware.ApiResponseWrapper
{
    public class ApiResponseWrapperMiddleware
    {
        private readonly RequestDelegate next;
        private readonly IApiResponseFactory apiResponseFactory;

        public ApiResponseWrapperMiddleware(RequestDelegate next, IApiResponseFactory apiResponseFactory)
        {
            this.next = next;
            this.apiResponseFactory = apiResponseFactory;
        }

        public async Task InvokeAsync(HttpContext context)
        {
            var processor = new ApiResponseProcessor(context, next, apiResponseFactory);
            await processor.ProcessAsync();
        }
    }
}
