using AddressBook.Api.Models.PhoneNumber.Request;
using FluentValidation;
using System;
using System.Collections.Generic;

namespace AddressBook.Api.Models.Contact.Request
{
    public class CreateContactRequestModel
    {
        /// <summary>
        /// Contact name. Combination of name and address must be unique.
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Birth date.
        /// </summary>
        public DateTime? BirthDate { get; set; }

        /// <summary>
        /// Contact address. Combination of name and address must be unique.
        /// </summary>
        public string Address { get; set; }

        /// <summary>
        /// Phone numbers.
        /// </summary>
        public List<CreatePhoneNumberRequestModel> PhoneNumbers { get; set; }

        public class Validator : AbstractValidator<CreateContactRequestModel>
        {
            public Validator()
            {
                RuleFor(x => x.Name).NotEmpty().Length(1, 100);
                RuleFor(x => x.Address).Length(1, 200);

                var validator = new CreatePhoneNumberRequestModel.Validator();
                RuleForEach(x => x.PhoneNumbers).SetValidator(validator);
            }
        }
    }
}
