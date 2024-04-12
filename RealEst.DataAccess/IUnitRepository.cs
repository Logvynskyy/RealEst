using RealEst.Core.Models;

namespace RealEst.DataAccess
{
    public interface IUnitRepository
    {
        Unit GetById(int id);
        List<Unit> GetAll();
        void Add(Unit unit);
        void Update(int id, Unit unit);
        bool DeleteById(int id);
    }
}
