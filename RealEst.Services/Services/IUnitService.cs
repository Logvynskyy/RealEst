using RealEst.Core.Models;

namespace RealEst.Services.Service
{
    public interface IUnitService
    {
        Unit GetById(int id);
        List<Unit> GetAll();
        bool Add(Unit unit);
        bool Update(int id, Unit unit);
        bool DeleteById(int id);
    }
}
