using AddressBook.Api.Models.Common.Request;

namespace AddressBook.Api.Models.Contact.Request
{
    public class ContactSearchRequestModel : PagingSortingBaseModel
    {
        public string Name { get; set; }
    }
}
