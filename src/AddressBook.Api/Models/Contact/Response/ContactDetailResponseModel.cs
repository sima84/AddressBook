using AddressBook.Api.Models.PhoneNumber.Response;
using System;
using System.Collections.Generic;

namespace AddressBook.Api.Models.Contact.Response
{
    public class ContactDetailResponseModel
    {
        /// <summary>
        /// Contact identifier.
        /// </summary>
        public long Id { get; set; }

        /// <summary>
        /// Name.
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Birth date.
        /// </summary>
        public DateTime? BirthDate { get; set; }

        /// <summary>
        /// Address.
        /// </summary>
        public string Address { get; set; }

        /// <summary>
        /// Phone numbers.
        /// </summary>
        public ICollection<PhoneNumberResponseModel> PhoneNumbers { get; set; }
    }
}
