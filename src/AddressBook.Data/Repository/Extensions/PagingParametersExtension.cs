using AddressBook.Data.Repository.Parameters;

namespace AddressBook.Data.Repository.Extensions
{
    public static class PagingParametersExtension
    {
        public static int GetSkipAmount(this PagingParameters pagingParameters)
        {
            return pagingParameters.CurrentPage > 1 ? pagingParameters.ItemsPerPage * (pagingParameters.CurrentPage - 1) : 0;
        }

        public static int GetTakeAmount(this PagingParameters pagingParameters)
        {
            return pagingParameters.ItemsPerPage > 0 ? pagingParameters.ItemsPerPage : 0;
        }
    }
}
