using AddressBook.Business.Models.Contact.Response;
using System.Threading.Tasks;

namespace AddressBook.Business.Services.Interfaces
{
    public interface IContactService
    {
        Task<ContactOverviewResponseModel> GetContactsAsync();
        Task<ContactDetailResponseModel> GetContactAsync(long id);
    }
}
