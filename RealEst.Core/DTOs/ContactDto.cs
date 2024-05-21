using RealEst.Core.Constants;

namespace RealEst.Core.DTOs
{
    public class ContactDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string PhoneNumber { get; set; }
        public ContactTypes ContactType { get; set; }
        public int Priority { get; set; }
    }
}
