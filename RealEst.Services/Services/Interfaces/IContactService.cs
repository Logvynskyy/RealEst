using RealEst.Core.Models;

namespace RealEst.Services.Services.Interfaces
{
    public interface IContactService
    {
        Contact GetById(int id);
        List<Contact> GetAll();
        bool Add(Contact contact);
        bool Update(int id, Contact contact);
        bool DeleteById(int id);
    }
}
