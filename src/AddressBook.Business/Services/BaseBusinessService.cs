using AddressBook.Business.Models.Common.Request;
using AddressBook.Data.Repository.Parameters;
using System.Collections.Generic;

namespace AddressBook.Business.Services
{
    public abstract class BaseBusinessService
    {
        /// <summary>
        /// String that we will receive in case of descending sort order
        /// </summary>
        protected const string DescendingSortOrder = "descending";

        protected virtual int GetDefaultItemsPerPage() => 10;

        protected virtual int GetDefaultCurrentPage() => 1;

        protected virtual SortingParameters GetDefaultSortingParameters() => null;

        /// <summary>
        /// Prepares the sort parameters.
        /// </summary>
        /// <param name="parameters">The parameters.</param>
        /// <param name="allowedSortBy">Fields that are allowed for sorting in overview.</param>
        /// <returns></returns>
        protected virtual SortingParameters PrepareSortParameters(PagingSortingBaseModel parameters, ICollection<string> allowedSortBy)
        {
            var isValidParameter = parameters.SortBy != null && allowedSortBy.Contains(parameters.SortBy.ToLower());

            if (isValidParameter)
            {
                return new SortingParameters
                {
                    SortField = parameters.SortBy,
                    SortReverse = parameters.Order == DescendingSortOrder
                };
            }

            var defaultSortingParameters = GetDefaultSortingParameters();
            if (defaultSortingParameters != null)
            {
                return defaultSortingParameters;
            }

            return SortingParameters.CreateNoSortingNeededParams();
        }

        /// <summary>
        /// Prepares the paging parameters.
        /// </summary>
        /// <param name="parameters">The parameters.</param>
        /// <returns></returns>
        protected virtual PagingParameters PreparePagingParameters(PagingSortingBaseModel parameters)
        {
            return new PagingParameters
            {
                CurrentPage = parameters.CurrentPage > 1 ? parameters.CurrentPage : GetDefaultCurrentPage(),
                ItemsPerPage = parameters.PageSize > 0 ? parameters.PageSize : GetDefaultItemsPerPage()
            };
        }
    }
}
