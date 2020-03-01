using AddressBook.Business.Models.PhoneNumber.Request;
using System;
using System.Collections.Generic;

namespace AddressBook.Business.Models.Contact.Request
{
    public class CreateContactRequestModel
    {
        public string Name { get; set; }
        public DateTime? BirthDate { get; set; }
        public string Address { get; set; }
        public List<CreatePhoneNumberRequestModel> PhoneNumbers { get; set; }
    }
}
