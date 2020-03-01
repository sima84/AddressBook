using AddressBook.Api.Models.Common.Request;

namespace AddressBook.Api.Models.Contact.Request
{
    public class ContactSearchRequestModel : PagingSortingBaseModel
    {
        /// <summary>
        /// Contact name search parameter. Will search for contact names that begin with given string
        /// </summary>
        public string Name { get; set; }
    }
}
