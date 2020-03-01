using AddressBook.Data.Repository.Parameters;
using System;

namespace AddressBook.Data.Repository.Extensions
{
    public static class TextSearchParametersExtension
    {
        public static string GetDynamicLinqWhereClauseExpression(this TextSearchParameters textSearchParameters)
        {
            if (string.IsNullOrWhiteSpace(textSearchParameters.SearchText))
            {
                throw new ArgumentException("Search text cannot be null or empty", nameof(textSearchParameters.SearchText));
            }

            if (textSearchParameters.SearchFields == null || textSearchParameters.SearchFields.Count == 0)
            {
                throw new ArgumentException("Search fields cannot be null or empty", nameof(textSearchParameters.SearchFields));
            }

            var responseExpression = string.Format("{0}.Contains(\"{1}\")", textSearchParameters.SearchFields[0], textSearchParameters.SearchText);

            for (var i = 1; i < textSearchParameters.SearchFields.Count; i++)
            {
                responseExpression += string.Format("or {0}.Contains(\"{1}\")", textSearchParameters.SearchFields[i], textSearchParameters.SearchText);
            }

            return responseExpression;
        }
    }
}
