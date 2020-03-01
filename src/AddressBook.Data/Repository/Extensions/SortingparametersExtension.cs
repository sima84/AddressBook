using AddressBook.Data.Repository.Parameters;
using System;

namespace AddressBook.Data.Repository.Extensions
{
    public static class SortingparametersExtension
    {
        public static string GetDynamicLinqOrderByClauseExpression(this SortingParameters orderingParameters)
        {
            if (string.IsNullOrWhiteSpace(orderingParameters.SortField))
            {
                throw new ArgumentException("Sort field cannot be null or empty", nameof(orderingParameters.SortField));
            }

            return string.Format("{0} {1}", orderingParameters.SortField, orderingParameters.SortReverse ? "desc" : "asc");
        }
    }
}
