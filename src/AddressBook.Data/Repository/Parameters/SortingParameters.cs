using System;
using System.Reflection;

namespace AddressBook.Data.Repository.Parameters
{
    public class SortingParameters
    {
        public static SortingParameters CreateNoSortingNeededParams()
        {
            return new SortingParameters()
            {
                SortField = null,
                SortReverse = false
            };
        }

        public string SortField { get; set; }

        public bool SortReverse { get; set; }

        public bool SortNeeded()
        {
            return !string.IsNullOrWhiteSpace(SortField);
        }

        public bool AreValidForType(Type type)
        {
            if (!SortNeeded())
            {
                return true;
            }

            var propertyInfo = type.GetProperty(
                SortField, BindingFlags.IgnoreCase |
                BindingFlags.Public |
                BindingFlags.Instance
            );

            return propertyInfo != null;
        }
    }
}
