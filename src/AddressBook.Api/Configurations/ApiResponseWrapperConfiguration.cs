using AddressBook.Api.Middleware.ApiResponseWrapper;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;

namespace AddressBook.Api.Configurations
{
    public static class ApiResponseWrapperConfiguration
    {
        public static void AddApiResponseWrapper(this IServiceCollection services)
        {
            services.AddTransient<IApiResponseFactory, ApiResponseFactory>();
        }

        public static void UseApiResponseWrapper(this IApplicationBuilder app)
        {
            app.UseMiddleware<ApiResponseWrapperMiddleware>();
        }
    }
}
