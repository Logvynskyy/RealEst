using RealEst.Core.Models;
using RealEst.Core.Constants;
using RealEst.DataAccess.Interfaces;

namespace RealEst.DataAccess.Implementations
{
    public class ListUnitRepository : IUnitRepository
    {
        private readonly List<Unit> _units = new()
        {
            new Unit("unit1", "address1", UnitTypes.Room, 15),
            new Unit("unit2", "address2", UnitTypes.Apartment, 60),
            new Unit("unit3", "address3", UnitTypes.House, 100, new List<Defect>()
                {
                    new Defect(0, "def1", "desc1", DefectTypes.InnerCosmetic)
                }
            )
        };

        private int _unitId = 3;

        public void Add(Unit unit)
        {
            _units.Add(unit);
        }

        public bool DeleteById(int id)
        {
            return _units.Remove(_units.FirstOrDefault(u => u.Id == id));
        }

        public List<Unit> GetAll()
        {
            return _units;
        }

        public Unit GetById(int id)
        {
            return _units.FirstOrDefault(u => u.Id == id);
        }

        public void Update(int id, Unit unit)
        {
            _units[id] = unit;
        }
    }
}
