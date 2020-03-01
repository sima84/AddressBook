using AddressBook.Api.Infrastructure.ApiResponse;

namespace AddressBook.Api.Middleware.ApiResponseWrapper
{
    public interface IApiResponseFactory
    {
        ApiResponse CreateApiResponse(int httpStatusCode, object httpBody);
    }
}
