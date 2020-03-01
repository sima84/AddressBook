using AddressBook.Api.Models.Contact.Response;
using AddressBook.Business.Services.Interfaces;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace AddressBook.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ContactsController : ControllerBase
    {
        private readonly IContactService service;
        private readonly IMapper mapper;

        public ContactsController(IContactService service, IMapper mapper)
        {
            this.service = service;
            this.mapper = mapper;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<ContactOverviewResponseModel>>> GetContacts()
        {
            var contacts = await service.GetContactsAsync();
            return Ok(mapper.Map<IEnumerable<ContactOverviewResponseModel>>(contacts));
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<ContactDetailResponseModel>> GetTagDetails(long id)
        {
            var contact = await service.GetContactAsync(id);
            return Ok(mapper.Map<ContactDetailResponseModel>(contact));
        }    
    }
}
