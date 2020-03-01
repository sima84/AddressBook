using AddressBook.Business.Models.PhoneNumber.Request;
using AddressBook.Business.Models.PhoneNumber.Response;
using AddressBook.Data.Entities;
using AutoMapper;

namespace AddressBook.Business.Mappings
{
    public class PhoneNumberProfile : Profile
    {
        public PhoneNumberProfile()
        {
            CreateMap<PhoneNumber, PhoneNumberResponseModel>();
            CreateMap<CreatePhoneNumberRequestModel, PhoneNumber>();
        }
    }
}
