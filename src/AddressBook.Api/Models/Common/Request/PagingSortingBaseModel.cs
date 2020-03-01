using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AddressBook.Api.Models.Common.Request
{
    public class PagingSortingBaseModel
    {
        /// <summary>
        /// Page number that should be returned
        /// </summary>
        public int CurrentPage { get; set; }

        /// <summary>
        /// Max number of items that should be returned on single page
        /// </summary>
        public int PageSize { get; set; }

        /// <summary>
        /// Field that should be used for sorting.
        /// </summary>
        public string SortBy { get; set; }

        /// <summary>
        /// Determines order of sorting. Possible values: ascending, descending
        /// </summary>
        public string Order { get; set; }
    }
}
