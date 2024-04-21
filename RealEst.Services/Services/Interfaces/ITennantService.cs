using RealEst.Core.Models;

namespace RealEst.Services.Services.Interfaces
{
    public interface ITennantService
    {
        Tennant GetById(int id);
        List<Tennant> GetAll();
        bool Add(Tennant tennant);
        bool Update(int id, Tennant tennant);
        bool DeleteById(int id);
    }
}
