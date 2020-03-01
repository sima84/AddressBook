using AddressBook.Api.Middleware.ExceptionToHttpResponseMapper;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;

namespace AddressBook.Api.Configurations
{
    public static class ExceptionToHttpResponseMapperConfiguration
    {
        public static void AddExceptionToHttpResponseMapper(this IServiceCollection services)
        {
            services.AddTransient<IExceptionToHttpResponseParametersMapper, ExceptionToHttpResponseParametersMapper>();
        }

        public static void UseExceptionToHttpResponseMapper(this IApplicationBuilder app)
        {
            app.UseMiddleware<ExceptionToHttpResponseMapperMiddleware>();
        }
    }
}
