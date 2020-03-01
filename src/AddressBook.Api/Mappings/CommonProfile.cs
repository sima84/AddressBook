using AddressBook.Api.Models.Common.Request;
using AddressBook.Api.Models.Common.Response;
using AutoMapper;
using BusinessRequests = AddressBook.Business.Models.Common.Request;
using BusinessResponses = AddressBook.Business.Models.Common.Response;

namespace AddressBook.Api.Mappings
{
    public class CommonProfile : Profile
    {
        public CommonProfile()
        {
            CreateMap<PagingSortingBaseModel, BusinessRequests.PagingSortingBaseModel>();
            CreateMap<BusinessResponses.PaginationDataResponse, PaginationDataResponse>();
            CreateMap(typeof(BusinessResponses.PagedResponse<>), typeof(PagedResponse<>));
        }
    }
}
