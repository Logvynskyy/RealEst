using RealEst.Core.Constants;
using RealEst.Core.DTOs;

namespace RealEst.Core.Models
{
    public class Contact : IPerson
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string PhoneNumber { get; set; }
        public ContactTypes ContactType { get; set; }
        public int Priority { get; set; }
        public Organisation Organisation { get; init; }

        public Contact()
        {
            
        }

        public Contact(string name, string lastName, string email, string phone, ContactTypes contactType, int priority)
        {
            Name = name;
            LastName = lastName;
            Email = email;
            PhoneNumber = phone;
            ContactType = contactType;
            Priority = priority;
        }

        public Contact(ContactDto contactDto, Organisation organisation)
            : this(contactDto.Name, contactDto.LastName, contactDto.Email, 
                  contactDto.PhoneNumber, contactDto.ContactType, contactDto.Priority)
        {
            Organisation = organisation;
        }
    }
}
