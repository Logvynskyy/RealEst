using RealEst.Core.Models;

namespace RealEst.DataAccess.Interfaces
{
    public interface IContractRepository
    {
        Contract GetById(int id);
        List<Contract> GetAll();
        void Add(Contract contract);
        void Update(int id, Contract contract);
        bool DeleteById(int id);
    }
}
