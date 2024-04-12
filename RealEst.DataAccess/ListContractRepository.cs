using RealEst.Core.Models;
using RealEst.Core.Constants;

namespace RealEst.DataAccess
{
    public class ListContractRepository : IContractRepository
    {
        private readonly List<Contract> _contracts = new()
        {
            new Contract(0, "contract1", 0, "abcd1", 0, 1000, new DateTime(), new DateTime()),
            new Contract(1, "contract2", 1, "abcd2", 1, 1500, new DateTime(), new DateTime()),
            new Contract(2, "contract3", 2, "abcd3", 2, 2000, new DateTime(), new DateTime())
        };

        private int _contractId = 3;

        public void Add(Contract contract)
        {
            contract.Id = _contractId++;
            _contracts.Add(contract);
        }

        public bool DeleteById(int id)
        {
            return _contracts.Remove(_contracts.FirstOrDefault(u => u.Id == id));
        }

        public List<Contract> GetAll()
        {
            return _contracts;
        }

        public Contract GetById(int id)
        {
            return _contracts.FirstOrDefault(u => u.Id == id);
        }

        public void Update(int id, Contract contract)
        {
            _contracts[id] = contract;
        }
    }
}
