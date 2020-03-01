namespace AddressBook.Api.Infrastructure.ApiResponse
{
    public class ApiResponse
    {
        private ApiResponse() { }

        public object Error { get; set; }

        public object Data { get; set; }

        public static ApiResponse FromData(object data)
        {
            return new ApiResponse()
            {
                Error = null,
                Data = data
            };
        }

        public static ApiResponse FromError(object error)
        {
            return new ApiResponse()
            {
                Error = error,
                Data = null
            };
        }
    }
}
