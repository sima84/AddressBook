using AddressBook.Api.Models.Common.Response;
using AddressBook.Api.Models.Contact.Request;
using AddressBook.Api.Models.Contact.Response;
using AddressBook.Business.Services.Interfaces;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using BusinessRequests = AddressBook.Business.Models.Contact.Request;

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
        public async Task<ActionResult<PagedResponse<ContactOverviewResponseModel>>> GetContacts([FromQuery]ContactSearchRequestModel request)
        {
            var businessRequest = mapper.Map<BusinessRequests.ContactSearchRequestModel>(request);
            var businessResponse = await service.GetContactsAsync(businessRequest);
            return Ok(mapper.Map<PagedResponse<ContactOverviewResponseModel>>(businessResponse));
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<ContactDetailResponseModel>> GetContactDetails(long id)
        {
            var contact = await service.GetContactAsync(id);
            return Ok(mapper.Map<ContactDetailResponseModel>(contact));
        }

        [HttpPost]
        public async Task<ActionResult<ContactDetailResponseModel>> CreateContact([FromBody] CreateContactRequestModel createRequest)
        {
            var businessRequest = mapper.Map<BusinessRequests.CreateContactRequestModel>(createRequest);
            var businessResponse = await service.CreateContactAsync(businessRequest);
            return CreatedAtAction(nameof(GetContactDetails), new { id = businessResponse.Id }, mapper.Map<ContactDetailResponseModel>(businessResponse));
        }

        [HttpPut("{id}")]
        public async Task<ActionResult<ContactDetailResponseModel>> UpdateContact([FromRoute]long id, [FromBody] UpdateContactRequestModel updateRequest)
        {
            var businessRequest = mapper.Map<BusinessRequests.UpdateContactRequestModel>(updateRequest);
            var businessResponse = await service.UpdateContactAsync(id, businessRequest);
            return Ok(mapper.Map<ContactDetailResponseModel>(businessResponse));
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> DeleteContact(long id)
        {
            await service.DeleteContactAsync(id);
            return NoContent();
        }
    }
}
