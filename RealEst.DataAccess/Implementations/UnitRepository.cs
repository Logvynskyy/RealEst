using Microsoft.EntityFrameworkCore;
using RealEst.Core.Models;
using RealEst.DataAccess.Interfaces;

namespace RealEst.DataAccess.Implementations
{
    public class UnitRepository : IUnitRepository
    {
        private readonly ApplicationContext _applicationContext;

        public UnitRepository(ApplicationContext applicationContext)
        {
            _applicationContext = applicationContext;
        }

        public void Add(Unit unit)
        {
            _applicationContext.Units.Add(unit);
            _applicationContext.SaveChanges();
        }

        public bool DeleteById(int id)
        {
            var unitToDelete = _applicationContext.Units
                .Remove(_applicationContext.Units.FirstOrDefault(u => u.Id == id)!);
            var entityState = unitToDelete.State;

            _applicationContext.SaveChanges();

            return entityState == EntityState.Deleted;
        }

        public List<Unit> GetAll()
        {
            return _applicationContext.Units
                    .Include(u => u.Defects)
                    .Include(u => u.Organisation)
                .ToList();
        }

        public Unit GetById(int id)
        {
            return _applicationContext.Units
                    .Include(u => u.Defects)
                    .Include(u => u.Organisation)
                .FirstOrDefault(u => u.Id == id)!;
        }

        public void Update(int id, Unit unit)
        {
            var unitToUpdate = _applicationContext.Units
                    .Include(u => u.Defects)
                    .Include(u => u.Organisation)
                .FirstOrDefault(_u => _u.Id == id);

            if (unitToUpdate != null)
            {
                unitToUpdate.Address = unit.Address;
                unitToUpdate.Defects = unit.Defects;
                unitToUpdate.Footage = unit.Footage;
                unitToUpdate.Name = unit.Name;
                unitToUpdate.UnitType = unit.UnitType;

                //_applicationContext.Units.Update(unitToUpdate);
                _applicationContext.SaveChanges();
            }
            else
            {
                throw new ArgumentOutOfRangeException("Entity with given ID wasn't found");
            }
        }
    }
}
