namespace AddressBook.Data.Entities
{
    public class PhoneNumber
    {
        public long Id { get; set; }
        public string Number { get; set; }
        public int Type { get; set; }

        public Contact Contact { get; set; }
    }
}
