using Microsoft.Extensions.Logging;
using RealEst.Core.Models;
using RealEst.DataAccess.Interfaces;
using RealEst.Services.Services.Interfaces;

namespace RealEst.Services.Services.Implementations
{
    public class ContractService : IContractService
    {
        private readonly IContractRepository _contractRepository;
        private readonly ILogger _logger;

        public ContractService(IContractRepository contractRepository, ILogger<ContractService> logger)
        {
            _contractRepository = contractRepository;
            _logger = logger;
        }

        public bool Add(Contract contract)
        {
            try
            {
                _contractRepository.Add(contract);

                _logger.LogInformation("Added new contract");
                return true;
            }
            catch (InvalidOperationException e)
            {
                _logger.LogError(e.Message);
                return false;
            }
        }

        public bool DeleteById(int id)
        {
            try
            {
                _logger.LogInformation("Deleting contract with id {0}", id);
                return _contractRepository.DeleteById(id);
            }
            catch (ArgumentOutOfRangeException e)
            {
                _logger.LogError(e.Message);
                return false;
            }
        }

        public List<Contract> GetAll()
        {
            try
            {
                _logger.LogInformation("Returned all contracts");
                return _contractRepository.GetAll();
            }
            catch (NullReferenceException e)
            {
                _logger.LogError(e.Message);
                return null;
            }
        }

        public Contract GetById(int id)
        {
            try
            {
                _logger.LogInformation("Getting contract with id {0}", id);
                return _contractRepository.GetById(id);
            }
            catch (ArgumentOutOfRangeException e)
            {
                _logger.LogError(e.Message);
                return null;
            }
        }

        public bool Update(int id, Contract contract)
        {
            try
            {
                // TODO: Add validation
                contract.Id = id;

                _contractRepository.Update(id, contract);

                _logger.LogInformation("Updating contract with id {0}", id);
                return true;
            }
            catch (InvalidOperationException e)
            {
                _logger.LogError(e.Message);
                return false;
            }
            catch (ArgumentOutOfRangeException e)
            {
                _logger.LogError(e.Message);
                return false;
            }
        }
    }
}
