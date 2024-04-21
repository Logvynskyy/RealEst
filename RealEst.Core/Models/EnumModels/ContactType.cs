using RealEst.Core.Constants;

namespace RealEst.Core.Models.EnumModels
{
    public class ContactType
    {
        public int Id { get; set; }
        public ContactTypes Code { get; set; }

        public ContactType()
        {

        }

        public ContactType(ContactTypes contactType)
        {
            Code = contactType;
        }
    }
}
