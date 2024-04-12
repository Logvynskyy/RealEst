using RealEst.Core.Models;

namespace RealEst.Services.Service
{
    public interface IContractService
    {
        Contract GetById(int id);
        List<Contract> GetAll();
        bool Add(Contract contract);
        bool Update(int id, Contract contract);
        bool DeleteById(int id);
    }
}
