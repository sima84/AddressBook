using System;
using System.Collections.Generic;
using System.Reflection;

namespace AddressBook.Data.Repository.Parameters
{
    public class TextSearchParameters
    {
        public static TextSearchParameters CreateNoTextSearchNeededParams()
        {
            return new TextSearchParameters()
            {
                SearchText = null,
                SearchFields = null
            };
        }

        public string SearchText { get; set; }

        public List<string> SearchFields { get; set; }

        public bool TextSearchNeeded()
        {
            return !string.IsNullOrWhiteSpace(SearchText) && SearchFields != null && SearchFields.Count > 0;
        }

        public bool AreValidForType(Type type)
        {
            if (!TextSearchNeeded())
            {
                return true;
            }

            foreach (var searchField in SearchFields)
            {
                bool isValidSearchField = IsValidSearchFieldForType(searchField, type);
                if (!isValidSearchField)
                {
                    return false;
                }
            }

            return true;
        }

        private bool IsValidSearchFieldForType(string searchField, Type type)
        {
            var parentType = type;
            var segments = searchField.Split('.');
            for (var i = 0; i < segments.Length; i++)
            {
                var propertyInfo = type.GetProperty(segments[i], BindingFlags.IgnoreCase | BindingFlags.Public | BindingFlags.Instance);
                if (propertyInfo == null)
                {
                    return false;
                }

                if (i == segments.Length - 1 && propertyInfo.PropertyType != typeof(string))
                {
                    return false;
                }

                parentType = propertyInfo.DeclaringType;
            }

            return true;
        }
    }
}
