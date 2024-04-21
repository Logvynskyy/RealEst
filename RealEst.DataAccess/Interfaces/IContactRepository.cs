using RealEst.Core.Models;

namespace RealEst.DataAccess.Interfaces
{
    public interface IContactRepository
    {
        Contact GetById(int id);
        List<Contact> GetAll();
        void Add(Contact contact);
        void Update(int id, Contact contact);
        bool DeleteById(int id);
    }
}
