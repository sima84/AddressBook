using AddressBook.Business.Models.Contact.Request;
using AddressBook.Business.Models.Contact.Response;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace AddressBook.Business.Services.Interfaces
{
    public interface IContactService
    {
        Task<IEnumerable<ContactOverviewResponseModel>> GetContactsAsync();
        Task<ContactDetailResponseModel> GetContactAsync(long id);
        Task<ContactDetailResponseModel> CreateContactAsync(CreateContactRequestModel requestModel);
        Task<ContactDetailResponseModel> UpdateContactAsync(long id, UpdateContactRequestModel requestModel);
        Task DeleteContactAsync(long id);
    }
}
