using FluentValidation;

namespace AddressBook.Api.Models.PhoneNumber.Request
{
    public class CreatePhoneNumberRequestModel
    {
        /// <summary>
        /// Phone number.
        /// </summary>
        public string Number { get; set; }

        public class Validator : AbstractValidator<CreatePhoneNumberRequestModel>
        {
            public Validator()
            {
                RuleFor(x => x.Number).NotEmpty().Length(1, 20);
            }
        }
    }
}
