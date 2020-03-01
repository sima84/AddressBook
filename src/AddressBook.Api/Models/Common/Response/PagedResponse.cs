using System.Collections.Generic;

namespace AddressBook.Api.Models.Common.Response
{
    public class PagedResponse<T>
    {
        public IEnumerable<T> Results { get; set; }
        public PaginationDataResponse Pagination { get; set; }
    }
}
