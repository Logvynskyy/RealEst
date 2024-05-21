using Microsoft.Extensions.Logging;
using RealEst.Core.DTOs;
using RealEst.Core.Models;
using RealEst.DataAccess.Interfaces;
using RealEst.Services.Services.Interfaces;

namespace RealEst.Services.Services.Implementations
{
    public class ContactService : IContactService
    {
        private readonly IContactRepository _contactRepository;
        private readonly ILogger _logger;
        private readonly IAuthenticationService _authenticationService;

        public ContactService(IContactRepository contactRepository, 
            ILogger<ContactService> logger,
            IAuthenticationService authenticationService)
        {
            _contactRepository = contactRepository;
            _logger = logger;
            _authenticationService = authenticationService;
        }

        public bool Add(ContactDto contact)
        {
            try
            {
                _contactRepository.Add(DtoToEntity(contact));

                _logger.LogInformation("Added new contact");
                return true;
            }
            catch (InvalidOperationException e)
            {
                _logger.LogError(e.Message);
                return false;
            }
        }

        public bool DeleteById(int id)
        {
            try
            {
                _logger.LogInformation("Deleting contact with id {0}", id);
                return _contactRepository.DeleteById(id);
            }
            catch (ArgumentOutOfRangeException e)
            {
                _logger.LogError(e.Message);
                return false;
            }
        }

        public List<ContactDto> GetAll()
        {
            try
            {
                _logger.LogInformation("Returned all contacts");
                return _contactRepository.GetAll().Select(x => EntityToDto(x)).ToList();
            }
            catch (NullReferenceException e)
            {
                _logger.LogError(e.Message);
                return null;
            }
        }

        public ContactDto GetById(int id)
        {
            try
            {
                _logger.LogInformation("Getting contact with id {0}", id);
                return EntityToDto(_contactRepository.GetById(id));
            }
            catch (ArgumentOutOfRangeException e)
            {
                _logger.LogError(e.Message);
                return null;
            }
        }

        public bool Update(int id, ContactDto contactDto)
        {
            try
            {
                // TODO: Add validation
                var contact = DtoToEntity(contactDto);

                _contactRepository.Update(id, contact);

                _logger.LogInformation("Updating contact with id {0}", id);
                return true;
            }
            catch (InvalidOperationException e)
            {
                _logger.LogError(e.Message);
                return false;
            }
            catch (ArgumentOutOfRangeException e)
            {
                _logger.LogError(e.Message);
                return false;
            }
        }

        public Contact DtoToEntity(ContactDto contactDto)
        {
            return new Contact(contactDto, _authenticationService.GetCurrentOrganisation());
        }

        public ContactDto EntityToDto(Contact entity)
        {
            return new ContactDto
            {
                Id = entity.Id,
                Name = entity.Name,
                LastName = entity.LastName,
                Email = entity.Email,
                PhoneNumber = entity.PhoneNumber,
                ContactType = entity.ContactType,
                Priority = entity.Priority
            };
        }
    }
}
