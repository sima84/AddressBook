using AddressBook.Api.Models.PhoneNumber.Response;
using AutoMapper;
using BusinessResponses = AddressBook.Business.Models.PhoneNumber.Response;
using BusinessRequests = AddressBook.Business.Models.PhoneNumber.Request;
using AddressBook.Api.Models.PhoneNumber.Request;

namespace AddressBook.Api.Mappings
{
    public class PhoneNumberProfile : Profile
    {
        public PhoneNumberProfile()
        {
            CreateMap<BusinessResponses.PhoneNumberResponseModel, PhoneNumberResponseModel>();
            CreateMap<CreatePhoneNumberRequestModel, BusinessRequests.CreatePhoneNumberRequestModel>();
            CreateMap<UpdatePhoneNumberRequestModel, BusinessRequests.UpdatePhoneNumberRequestModel>();
        }
    }
}
