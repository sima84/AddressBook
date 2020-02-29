namespace AddressBook.Data.Entities
{
    public class PhoneNumber
    {
        public long Id { get; set; }
        public string Number { get; set; }
        public long ContactId { get; set; }

        public Contact Contact { get; set; }
    }
}
