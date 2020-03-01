using AddressBook.Business.Models.Contact.Response;
using AddressBook.Business.Services.Interfaces;
using System;
using System.Threading.Tasks;

namespace AddressBook.Business.Services
{
    public class ContactService : IContactService
    {
        public Task<ContactOverviewResponseModel> GetContactsAsync()
        {
            throw new NotImplementedException();
        }

        public Task<ContactDetailResponseModel> GetContactAsync(long id)
        {
            throw new NotImplementedException();
        }
    }
}
