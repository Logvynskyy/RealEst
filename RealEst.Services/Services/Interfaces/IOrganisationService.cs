using RealEst.Core.Models;

namespace RealEst.Services.Services.Interfaces
{
    public interface IOrganisationService
    {
        Organisation GetById(int id);
        bool Add(Organisation organisation);
    }
}
