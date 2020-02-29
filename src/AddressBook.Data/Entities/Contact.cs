using System;
using System.Collections.Generic;

namespace AddressBook.Data.Entities
{
    public class Contact
    {
        public Contact()
        {
            PhoneNumbers = new HashSet<PhoneNumber>();
        }
        public long Id { get; set; }
        public string Name { get; set; }
        public DateTime? BirthDate { get; set; }
        public string Address { get; set; }

        public ICollection<PhoneNumber> PhoneNumbers { get; set; }
    }
}
