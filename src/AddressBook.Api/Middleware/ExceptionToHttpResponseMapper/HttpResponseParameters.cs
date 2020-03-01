namespace AddressBook.Api.Middleware.ExceptionToHttpResponseMapper
{
    public class HttpResponseParameters
    {
        public int StatusCode { get; set; }
        public string Content { get; set; }
        public string ContentType { get; set; }
    }
}
