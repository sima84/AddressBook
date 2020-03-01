using AddressBook.Business.Models.PhoneNumber.Response;
using System;
using System.Collections.Generic;

namespace AddressBook.Business.Models.Contact.Response
{
    public class ContactDetailResponseModel
    {
        public long Id { get; set; }
        public string Name { get; set; }
        public DateTime? BirthDate { get; set; }
        public string Address { get; set; }
        public ICollection<PhoneNumberResponseModel> PhoneNumbers { get; set; }
    }
}
