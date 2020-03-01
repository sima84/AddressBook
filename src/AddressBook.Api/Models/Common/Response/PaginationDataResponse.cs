namespace AddressBook.Api.Models.Common.Response
{
    public class PaginationDataResponse
    {
        public int Page { get; set; }
        public int PageSize { get; set; }
        public int PageCount { get; set; }
        public int ResultCount { get; set; }
    }
}
