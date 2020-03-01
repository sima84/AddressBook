using FluentValidation;

namespace AddressBook.Api.Models.PhoneNumber.Request
{
    public class UpdatePhoneNumberRequestModel
    {
        /// <summary>
        /// Phone number identifier.
        /// </summary>
        public long Id { get; set; }

        /// <summary>
        /// Phone number.
        /// </summary>
        public string Number { get; set; }

        public class Validator : AbstractValidator<UpdatePhoneNumberRequestModel>
        {
            public Validator()
            {
                RuleFor(x => x.Id).GreaterThanOrEqualTo(0);
                RuleFor(x => x.Number).NotEmpty().Length(1, 20);
            }
        }
    }
}
