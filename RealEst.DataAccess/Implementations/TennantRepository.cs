using Microsoft.EntityFrameworkCore;
using RealEst.Core.Models;
using RealEst.DataAccess.Interfaces;

namespace RealEst.DataAccess.Implementations
{
    public class TennantRepository : ITennantRepository
    {
        private readonly ApplicationContext _applicationContext;

        public TennantRepository(ApplicationContext applicationContext)
        {
            _applicationContext = applicationContext;
        }

        public void Add(Tennant tennant)
        {
            _applicationContext.Tennants.Add(tennant);
            _applicationContext.SaveChanges();
        }

        public bool DeleteById(int id)
        {
            var tennantToDelete = _applicationContext.Tennants
                .Remove(_applicationContext.Tennants.FirstOrDefault(t => t.Id == id)!);
            var entityState = tennantToDelete.State;

            _applicationContext.SaveChanges();

            return entityState == EntityState.Deleted;
        }

        public List<Tennant> GetAll()
        {
            return _applicationContext.Tennants.Include(t => t.Organisation).ToList();
        }

        public Tennant GetById(int id)
        {
            return _applicationContext.Tennants.Include(t => t.Organisation).FirstOrDefault(t => t.Id == id)!;
        }

        public void Update(int id, Tennant tennant)
        {
            var tennantToUpdate = _applicationContext.Tennants.Include(t => t.Organisation).FirstOrDefault(c => c.Id == id);

            if (tennantToUpdate != null)
            {
                tennantToUpdate.Name = tennant.Name;
                tennantToUpdate.Email = tennant.Email;
                tennantToUpdate.Debt = tennant.Debt;
                tennantToUpdate.LastName = tennant.LastName;

                //_applicationContext.Tennants.Update(contractToUpdate);
                _applicationContext.SaveChanges();
            }
            else
            {
                throw new ArgumentOutOfRangeException("Entity with given ID wasn't found");
            }
        }
    }
}
