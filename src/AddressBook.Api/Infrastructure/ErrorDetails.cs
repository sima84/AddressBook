using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;

namespace AddressBook.Api.Infrastructure
{
    public class ErrorDetails
    {
        public const string DefaultErrorMessage = "An error has occurred!";

        private ErrorDetails()
        {

        }

        public bool SystemError { get; set; }

        public string ErrorMessage { get; set; }

        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        public object ErrorObject { get; set; }

        public static ErrorDetails FromErrorObject(object errorObject, bool systemError = false)
        {
            return new ErrorDetails()
            {
                SystemError = systemError,
                ErrorMessage =
                    errorObject is string && errorObject != null ? errorObject.ToString() : DefaultErrorMessage,
                ErrorObject =
                    errorObject is string ? null : errorObject
            };
        }
    }
}
