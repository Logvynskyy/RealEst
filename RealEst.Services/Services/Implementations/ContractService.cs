using Microsoft.Extensions.Logging;
using RealEst.Core.DTOs;
using RealEst.Core.Models;
using RealEst.DataAccess.Interfaces;
using RealEst.Services.Services.Interfaces;

namespace RealEst.Services.Services.Implementations
{
    public class ContractService : IContractService
    {
        private readonly IContractRepository _contractRepository;
        private readonly ILogger _logger;
        private readonly IAuthenticationService _authenticationService;
        private readonly IUnitService _unitService;
        private readonly ITennantService _tennantService;

        public ContractService(IContractRepository contractRepository, 
            ILogger<ContractService> logger,
            IAuthenticationService authenticationService,
            IUnitService unitService,
            ITennantService tennantService)
        {
            _contractRepository = contractRepository;
            _logger = logger;
            _authenticationService = authenticationService;
            _unitService = unitService;
            _tennantService = tennantService;
        }

        public bool Add(ContractInputDto contractDto)
        {
            try
            {
                _contractRepository.Add(DtoToEntity(contractDto));

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

        public List<ContractOutputDto> GetAll()
        {
            try
            {
                _logger.LogInformation("Returned all contracts");

                return _contractRepository.GetAll().Select(x => EntityToDto(x)).ToList();
            }
            catch (NullReferenceException e)
            {
                _logger.LogError(e.Message);
                return null;
            }
        }

        public ContractOutputDto GetById(int id)
        {
            try
            {
                _logger.LogInformation("Getting contract with id {0}", id);

                return EntityToDto(_contractRepository.GetById(id));
            }
            catch (ArgumentOutOfRangeException e)
            {
                _logger.LogError(e.Message);
                return null;
            }
        }

        public bool Update(int id, ContractInputDto contractDto)
        {
            try
            {
                // TODO: Add validation
                var contract = DtoToEntity(contractDto);

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

        public Contract DtoToEntity(ContractInputDto contractDto)
        {
            return new Contract(contractDto, _authenticationService.GetCurrentOrganisation());
        }

        public ContractOutputDto EntityToDto(Contract contract)
        {
            return new ContractOutputDto
            {
                Id = contract.Id,
                Name = contract.Name,
                Unit = contract.Unit.DisplayString,
                Iban = contract.Iban,
                Tennant = contract.Tennant.DisplayString,
                Price = contract.Price,
                RentFrom = contract.RentFrom,
                RentTo = contract.RentTo
            };
        }
    }
}
