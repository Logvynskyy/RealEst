using RealEst.Core.Models;

namespace RealEst.DataAccess.Interfaces
{
    public interface IOrganisationRepository
    {
        Organisation GetById(int id);
        void Add(Organisation organisation);
    }
}
