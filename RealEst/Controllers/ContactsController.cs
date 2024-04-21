using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using RealEst.Core.Models;
using RealEst.Services.Services.Interfaces;

namespace RealEst.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ContactsController : ControllerBase
    {
        private readonly IContactService _contactService;

        public ContactsController(IContactService contactService)
        {
            _contactService = contactService;
        }

        [HttpGet]
        public IActionResult Index()
        {
            var contacts = _contactService.GetAll();

            if (contacts == null || contacts.Count == 0)
                return NotFound("You don't have any contacts! Please, create one");

            return Ok(contacts);
        }

        [HttpGet("{id}")]
        public IActionResult GetContactById(int id)
        {
            var contact = _contactService.GetById(id);

            if (contact == null)
                return NotFound("You entered wrong contact ID!");

            return Ok(contact);
        }

        [HttpPost("create")]
        public IActionResult CreateNewContact([FromBody] Contact contact)
        {
            if (!_contactService.Add(contact))
                return NotFound("Something went wrong!");

            return Created("api/Contacts", contact);
        }

        [HttpDelete("delete/{id}")]
        public IActionResult DeleteContact(int id)
        {
            if (!_contactService.DeleteById(id))
                return NotFound("You entered wrong contact ID!");

            return Ok(_contactService.GetAll());
        }

        [HttpPatch("edit/{id}")]
        public IActionResult UpdateContact(int id, [FromBody] Contact contact)
        {
            if (!_contactService.Update(id, contact))
                return NotFound("You entered wrong contact ID!");

            return Ok(_contactService.GetById(id));
        }
    }
}
