using RealEst.Core.Models;
using RealEst.DataAccess.Interfaces;

namespace RealEst.DataAccess.Implementations
{
    public class OrganisationRepository : IOrganisationRepository
    {
        private ApplicationContext _applicationContext;

        public OrganisationRepository(ApplicationContext applicationContext)
        {
            _applicationContext = applicationContext;
        }

        public void Add(Organisation organisation)
        {
            _applicationContext.Organisations.Add(organisation);
            _applicationContext.SaveChanges();
        }

        public Organisation GetById(int id)
        {
            return _applicationContext.Organisations.FirstOrDefault(o => o.Id == id)!;
        }
    }
}
