using AddressBook.Business.Models.Common.Request;

namespace AddressBook.Business.Models.Contact.Request
{
    public class ContactSearchRequestModel : PagingSortingBaseModel
    {
        public string Name { get; set; }
    }
}
