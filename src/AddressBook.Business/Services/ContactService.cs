using AddressBook.Business.Models.Contact.Request;
using AddressBook.Business.Models.Contact.Response;
using AddressBook.Business.Models.PhoneNumber.Request;
using AddressBook.Business.Services.Interfaces;
using AddressBook.Data.Entities;
using AddressBook.Data.Repository;
using AutoMapper;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AddressBook.Business.Services
{
    public class ContactService : IContactService
    {
        private readonly IRepository<Contact> contactRepository;
        private readonly IRepository<PhoneNumber> phoneNumberRepository;
        private readonly IMapper mapper;

        public ContactService(IRepository<Contact> contactRepository, IRepository<PhoneNumber> phoneNumberRepository, IMapper mapper)
        {
            this.contactRepository = contactRepository;
            this.phoneNumberRepository = phoneNumberRepository;
            this.mapper = mapper;
        }

        public async Task<IEnumerable<ContactOverviewResponseModel>> GetContactsAsync()
        {
            var contacts = await contactRepository.GetAllAsync();
            return mapper.Map<IEnumerable<ContactOverviewResponseModel>>(contacts);
        }

        public async Task<ContactDetailResponseModel> GetContactAsync(long id)
        {
            var contact = await GetContactWithPhoneNumbers(id);
            return mapper.Map<ContactDetailResponseModel>(contact);
        }

        public async Task<ContactDetailResponseModel> CreateContactAsync(CreateContactRequestModel requestModel)
        {
            var contact = mapper.Map<Contact>(requestModel);

            await contactRepository.AddAsync(contact);
            await contactRepository.SaveAsync();

            return mapper.Map<ContactDetailResponseModel>(contact);
        }

        public async Task<ContactDetailResponseModel> UpdateContactAsync(long id, UpdateContactRequestModel requestModel)
        {
            var contact = await GetContactWithPhoneNumbers(id);
            contact = mapper.Map(requestModel, contact);

            await UpdatePhoneNumbers(contact, requestModel);

            await contactRepository.SaveAsync();
            return mapper.Map<ContactDetailResponseModel>(contact);
        }

        public async Task DeleteContactAsync(long id)
        {
            var contact = await GetContactWithPhoneNumbers(id);
            
            foreach (var phoneNumber in contact.PhoneNumbers)
            {
                phoneNumberRepository.Delete(phoneNumber);
            }

            contactRepository.Delete(contact);
            await contactRepository.SaveAsync();
        }

        private async Task<Contact> GetContactWithPhoneNumbers(long id)
        {
            var contact = await contactRepository.GetOneAsync
                (
                    filter: x => x.Id == id,
                    include: query => query.Include(x => x.PhoneNumbers)
                );

            if (contact == null)
                throw new ArgumentOutOfRangeException($"Contact with id {id} does not exists");

            return contact;
        }

        private async Task UpdatePhoneNumbers(Contact contact, UpdateContactRequestModel requestModel)
        {
            DeleteSurplusPhoneNumbers(contact, requestModel.PhoneNumbers);
            await AddOrUpdatePhoneNumbers(contact, requestModel.PhoneNumbers);
        }

        private void DeleteSurplusPhoneNumbers(Contact contact, List<UpdatePhoneNumberRequestModel> updatedPhoneNumbers)
        {
            var updatedPhoneNumberIds = updatedPhoneNumbers.Select(x => x.Id);

            foreach (var phoneNumber in contact.PhoneNumbers)
            {
                if (!updatedPhoneNumberIds.Contains(phoneNumber.Id))
                {
                    phoneNumberRepository.Delete(phoneNumber);
                }
            }
        }

        private async Task AddOrUpdatePhoneNumbers(Contact contact, List<UpdatePhoneNumberRequestModel> updatedPhoneNumbers)
        {
            for(var i = 0; i < updatedPhoneNumbers.Count; i++)
            {
                //Add
                if (updatedPhoneNumbers[i].Id == default(long))
                {
                    var newPhoneNumber = new PhoneNumber 
                    { 
                        Number = updatedPhoneNumbers[i].Number,
                        ContactId = contact.Id
                    };
                    await phoneNumberRepository.AddAsync(newPhoneNumber);
                }
                //Update
                else
                {
                    var existingPhoneNumber = contact.PhoneNumbers.FirstOrDefault(x => x.Id == updatedPhoneNumbers[i].Id);

                    if (existingPhoneNumber == null)
                        throw new ArgumentOutOfRangeException($"Phone number with id {updatedPhoneNumbers[i].Id} does not exist for contact {contact.Id}");

                    existingPhoneNumber.Number = updatedPhoneNumbers[i].Number;
                }
            }
        }
    }
}
