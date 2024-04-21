using Microsoft.Extensions.Logging;
using RealEst.Core.Models;
using RealEst.DataAccess.Interfaces;
using RealEst.Services.Services.Interfaces;

namespace RealEst.Services.Services.Implementations
{
    public class ContactService : IContactService
    {
        private readonly IContactRepository _contactRepository;
        private readonly ILogger _logger;

        public ContactService(IContactRepository contactRepository, ILogger<ContactService> logger)
        {
            _contactRepository = contactRepository;
            _logger = logger;
        }

        public bool Add(Contact contact)
        {
            try
            {
                _contactRepository.Add(contact);

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

        public List<Contact> GetAll()
        {
            try
            {
                _logger.LogInformation("Returned all contacts");
                return _contactRepository.GetAll();
            }
            catch (NullReferenceException e)
            {
                _logger.LogError(e.Message);
                return null;
            }
        }

        public Contact GetById(int id)
        {
            try
            {
                _logger.LogInformation("Getting contact with id {0}", id);
                return _contactRepository.GetById(id);
            }
            catch (ArgumentOutOfRangeException e)
            {
                _logger.LogError(e.Message);
                return null;
            }
        }

        public bool Update(int id, Contact contact)
        {
            try
            {
                // TODO: Add validation
                contact.Id = id;

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
    }
}
