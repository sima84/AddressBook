using AddressBook.Api.Models.Contact.Response;
using AutoMapper;
using BusinessResponses = AddressBook.Business.Models.Contact.Response;
using BusinessRequests = AddressBook.Business.Models.Contact.Request;
using AddressBook.Api.Models.Contact.Request;

namespace AddressBook.Api.Mappings
{
    public class ContactProfile : Profile
    {
        public ContactProfile()
        {
            CreateMap<BusinessResponses.ContactOverviewResponseModel, ContactOverviewResponseModel>();
            CreateMap<BusinessResponses.ContactDetailResponseModel, ContactDetailResponseModel>();
            CreateMap<CreateContactRequestModel, BusinessRequests.CreateContactRequestModel>();
            CreateMap<UpdateContactRequestModel, BusinessRequests.UpdateContactRequestModel>();
            CreateMap<ContactSearchRequestModel, BusinessRequests.ContactSearchRequestModel>();
        }
    }
}
