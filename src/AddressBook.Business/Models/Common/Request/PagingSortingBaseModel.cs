namespace AddressBook.Business.Models.Common.Request
{
    public class PagingSortingBaseModel
    {
        public int CurrentPage { get; set; }
        public int PageSize { get; set; }
        public string SortBy { get; set; }
        public string Order { get; set; }
    }
}
