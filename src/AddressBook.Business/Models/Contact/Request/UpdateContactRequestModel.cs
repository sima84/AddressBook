using AddressBook.Business.Models.PhoneNumber.Request;
using System;
using System.Collections.Generic;

namespace AddressBook.Business.Models.Contact.Request
{
    public class UpdateContactRequestModel
    {
        public string Name { get; set; }
        public DateTime? BirthDate { get; set; }
        public string Address { get; set; }
        public List<UpdatePhoneNumberRequestModel> PhoneNumbers { get; set; }
    }
}
