using AddressBook.Business.Models.Common.Response;
using AddressBook.Business.Models.Contact.Request;
using AddressBook.Business.Models.Contact.Response;
using System.Threading.Tasks;

namespace AddressBook.Business.Services.Interfaces
{
    public interface IContactService
    {
        Task<PagedResponse<ContactOverviewResponseModel>> GetContactsAsync(ContactSearchRequestModel requestModel);
        Task<ContactDetailResponseModel> GetContactAsync(long id);
        Task<ContactDetailResponseModel> CreateContactAsync(CreateContactRequestModel requestModel);
        Task<ContactDetailResponseModel> UpdateContactAsync(long id, UpdateContactRequestModel requestModel);
        Task DeleteContactAsync(long id);
    }
}
