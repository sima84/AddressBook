using System;
using System.Collections.Generic;
using System.Linq;

namespace AddressBook.Business.Exceptions
{
    public class BusinessException : Exception
    {
        public BusinessException(string message) : base(message)
        {
            Errors = new Dictionary<string, List<string>>();
        }

        public BusinessException(string message, string propertyPath) : base(message)
        {
            Errors = new Dictionary<string, List<string>>();
            Errors.Add(propertyPath, new List<string> { message });
        }

        public void AddError(string propertyPath, params string[] errors)
        {
            Errors.Add(propertyPath, errors.ToList());
        }

        /// <summary>
        /// Dictionary which stores the list of errors for a property path
        /// </summary>
        public Dictionary<string, List<string>> Errors { get; set; }
    }
}
