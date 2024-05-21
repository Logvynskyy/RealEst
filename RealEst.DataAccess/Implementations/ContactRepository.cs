using Microsoft.EntityFrameworkCore;
using RealEst.Core.Models;
using RealEst.DataAccess.Interfaces;

namespace RealEst.DataAccess.Implementations
{
    public class ContactRepository : IContactRepository
    {
        private readonly ApplicationContext _applicationContext;

        public ContactRepository(ApplicationContext applicationContext)
        {
            _applicationContext = applicationContext;
        }
        public void Add(Contact contact)
        {
            _applicationContext.Contacts.Add(contact);
            _applicationContext.SaveChanges();
        }

        public bool DeleteById(int id)
        {
            var contactToDelete = _applicationContext.Contacts
                .Remove(_applicationContext.Contacts.FirstOrDefault(c => c.Id == id)!);
            var entityState = contactToDelete.State;

            _applicationContext.SaveChanges();

            return entityState == EntityState.Deleted;
        }

        public List<Contact> GetAll()
        {
            return _applicationContext.Contacts.Include(c => c.Organisation).ToList();
        }

        public Contact GetById(int id)
        {
            return _applicationContext.Contacts.Include(c => c.Organisation).FirstOrDefault(c => c.Id == id)!;
        }

        public void Update(int id, Contact contact)
        {
            var contactToUpdate = _applicationContext.Contacts.Include(c => c.Organisation).FirstOrDefault(c => c.Id == id);

            if (contactToUpdate != null)
            {
                contactToUpdate.Email = contact.Email;
                contactToUpdate.Priority = contact.Priority;
                contactToUpdate.Name = contact.Name;
                contactToUpdate.PhoneNumber = contact.PhoneNumber;
                contactToUpdate.LastName = contact.LastName;
                contactToUpdate.ContactType = contact.ContactType;

                //_applicationContext.Contacts.Update(contractToUpdate);
                _applicationContext.SaveChanges();
            }
            else
            {
                throw new ArgumentOutOfRangeException("Entity with given ID wasn't found");
            }
        }
    }
}
