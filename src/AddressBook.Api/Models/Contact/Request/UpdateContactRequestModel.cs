using AddressBook.Api.Models.PhoneNumber.Request;
using System;
using System.Collections.Generic;

namespace AddressBook.Api.Models.Contact.Request
{
    public class UpdateContactRequestModel
    {
        public string Name { get; set; }
        public DateTime? BirthDate { get; set; }
        public string Address { get; set; }
        public List<UpdatePhoneNumberRequestModel> PhoneNumbers { get; set; }
    }
}
