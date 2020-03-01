using System.Collections.Generic;

namespace AddressBook.Business.Models.Common.Response
{
    public class PagedResponse<T>
    {
        public IEnumerable<T> Results { get; set; }
        public PaginationDataResponse Pagination { get; set; }
    }
}
