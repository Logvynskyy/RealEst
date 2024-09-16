using RealEst.Core.Models;

namespace RealEst.DataAccess.Interfaces
{
    public interface ITennantRepository
    {
        Tennant GetById(int id);
        List<Tennant> GetAll();
        void Add(Tennant tennant);
        void Update(int id, Tennant tennant);
        bool DeleteById(int id);
    }
}
