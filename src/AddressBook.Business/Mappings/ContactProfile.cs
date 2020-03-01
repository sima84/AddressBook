using AddressBook.Business.Models.Contact.Request;
using AddressBook.Business.Models.Contact.Response;
using AddressBook.Data.Entities;
using AutoMapper;

namespace AddressBook.Business.Mappings
{
    public class ContactProfile : Profile
    {
        public ContactProfile()
        {
            CreateMap<Contact, ContactOverviewResponseModel>();
            CreateMap<Contact, ContactDetailResponseModel>();
            CreateMap<CreateContactRequestModel, Contact>();
            CreateMap<UpdateContactRequestModel, Contact>()
                .ForMember(dest => dest.PhoneNumbers, opt => opt.Ignore());
        }
    }
}
