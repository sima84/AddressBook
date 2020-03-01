using AddressBook.Api.Infrastructure.Validation;
using AddressBook.Api.Models.Contact.Request;
using FluentValidation;
using FluentValidation.AspNetCore;
using Microsoft.Extensions.DependencyInjection;

namespace AddressBook.Api.Configurations
{
    public static class ValidationConfiguration
    {
        public static IMvcBuilder AddFluentValidationConfiguration(this IMvcBuilder mvc)
        {
            mvc.AddFluentValidation(fv =>
            {
                fv.RunDefaultMvcValidationAfterFluentValidationExecutes = false;
                fv.RegisterValidatorsFromAssemblyContaining<CreateContactRequestModel>();
            });

            ValidatorOptions.PropertyNameResolver = CamelCasePropertyNameResolver.ResolvePropertyName;
            return mvc;
        }
    }
}
