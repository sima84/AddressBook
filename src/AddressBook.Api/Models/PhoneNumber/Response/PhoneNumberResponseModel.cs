namespace AddressBook.Api.Models.PhoneNumber.Response
{
    public class PhoneNumberResponseModel
    {
        /// <summary>
        /// Phone number identifier.
        /// </summary>
        public long Id { get; set; }

        /// <summary>
        /// Phone number.
        /// </summary>
        public string Number { get; set; }
    }
}
