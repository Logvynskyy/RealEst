using RealEst.Core.Models;
using RealEst.DataAccess;

namespace RealEst.Services.Service
{
    public class ContractService : IContractService
    {
        private readonly IContractRepository _contractRepository;

        public ContractService(IContractRepository contractRepository)
        {
            _contractRepository = contractRepository;
        }

        public bool Add(Contract contract)
        {
            try
            {
                _contractRepository.Add(contract);
                return true;
            }
            catch (InvalidOperationException e)
            {
                Console.WriteLine(e.Message);
                return false;
            }
        }

        public bool DeleteById(int id)
        {
            try
            {
                return _contractRepository.DeleteById(id);
            }
            catch (ArgumentOutOfRangeException e)
            {
                Console.WriteLine(e.Message);
                return false;
            }
        }

        public List<Contract> GetAll()
        {
            try
            {
                return _contractRepository.GetAll();
            }
            catch (NullReferenceException e)
            {
                Console.WriteLine(e.Message);
                return null;
            }
        }

        public Contract GetById(int id)
        {
            try
            {
                return _contractRepository.GetById(id);
            }
            catch (ArgumentOutOfRangeException e)
            {
                Console.WriteLine(e.Message);
                return null;
            }
        }

        public bool Update(int id, Contract contract)
        {
            try
            {
                // TODO: Add validation

                _contractRepository.Update(id, contract);
                return true;
            }
            catch (InvalidOperationException e)
            {
                Console.WriteLine(e.Message);
                return false;
            }
            catch (ArgumentOutOfRangeException e)
            {
                Console.WriteLine(e.Message);
                return false;
            }
        }
    }
}
