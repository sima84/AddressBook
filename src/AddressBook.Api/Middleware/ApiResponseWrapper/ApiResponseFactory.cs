using AddressBook.Api.Infrastructure.ApiResponse;

namespace AddressBook.Api.Middleware.ApiResponseWrapper
{
    public class ApiResponseFactory : IApiResponseFactory
    {
        public ApiResponse CreateApiResponse(int httpStatusCode, object httpBody)
        {
            if (IsSuccessStatusCode(httpStatusCode))
            {
                return ApiResponse.FromData(httpBody);
            }
            else
            {
                return ApiResponse.FromError(httpBody);
            }
        }

        private bool IsSuccessStatusCode(int statusCode)
        {
            return (statusCode >= 200) && (statusCode <= 299);
        }
    }
}
