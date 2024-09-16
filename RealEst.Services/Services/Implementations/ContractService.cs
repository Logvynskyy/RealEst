using Microsoft.Extensions.Logging;
using RealEst.Core.DTOs;
using RealEst.Core.Models;
using RealEst.DataAccess.Interfaces;
using RealEst.Services.Services.Interfaces;
using System.Globalization;

namespace RealEst.Services.Services.Implementations
{
    public class ContractService : IContractService
    {
        private readonly IContractRepository _contractRepository;
        private readonly ILogger _logger;
        private readonly IAuthenticationService _authenticationService;
        private readonly IUnitService _unitService;
        private readonly ITennantService _tennantService;
        private readonly Organisation _currentOrganisation;

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
            _currentOrganisation = _authenticationService.GetCurrentOrganisation();
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

                return _contractRepository.GetAll()
                    .Where(c => c.Organisation == _currentOrganisation)
                    .Select(x => EntityToDto(x)).ToList();
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

        public bool Update(int id, ContractEditDto contractDto)
        {
            try
            {
                // TODO: Add validation
                var contract = EditDtoToEntity(id, contractDto);

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

        public List<IncomeDto> GetIncome()
        {
            var income = new List<IncomeDto>();
            var contracts = GetAll();

            var months = GetAllMonthsBetweenDates(contracts.Min(x => x.RentFrom), contracts.Max(x => x.RentTo));

            foreach(var month in months)
            {
                var amount = GetContractsThatAreValidInMonth(month)?.Sum(x => x.Price);

                income.Add(new IncomeDto
                {
                    Month = CultureInfo.CreateSpecificCulture("uk-UA").DateTimeFormat.GetMonthName(month.Month) + " " + month.Year,
                    Amount = amount ?? 0,
                });
            }
            
            return income.Where(x => x.Amount > 0).ToList();
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
                Unit = _unitService.EntityToDto(contract.Unit).DisplayString,
                Iban = contract.Iban,
                Tennant = _tennantService.EntityToDto(contract.Tennant).DisplayString,
                Price = contract.Price,
                RentFrom = contract.RentFrom,
                RentTo = contract.RentTo
            };
        }

        private Contract EditDtoToEntity(int id, ContractEditDto contractDto)
        {
            return DtoToEntity(new ContractInputDto
            {
                Id = id,
                Name = contractDto.Name,
                UnitId = _contractRepository.GetById(id).UnitId,
                Iban = contractDto.Iban,
                TennantId = _contractRepository.GetById(id).TennantId,
                Price = contractDto.Price,
                RentFrom = GetById(id).RentFrom,
                RentTo = contractDto.RentTo
            });
        }

        private List<DateTime> GetAllMonthsBetweenDates(DateTime startDate, DateTime endDate)
        {
            var months = new List<DateTime>();
            var current = new DateTime(startDate.Year, startDate.Month, 1);

            while (current <= endDate)
            {
                months.Add(current);
                current = current.AddMonths(1);
            }

            return months;
        }

        private List<ContractOutputDto> GetContractsThatAreValidInMonth(DateTime date)
        {
            int year = date.Year;
            int month = date.Month;
            var contracts = GetAll();
            var validContracts = new List<ContractOutputDto>();

            contracts.ForEach(contract =>
            {
                bool isRentFromValid = contract.RentFrom.Year == year && contract.RentFrom.Month == month;
                bool isRentToValid = contract.RentTo.Year == year && contract.RentTo.Month == month;

                bool isSpanningMonth = contract.RentFrom <= new DateTime(year, month, DateTime.DaysInMonth(year, month)) &&
                                       contract.RentTo >= new DateTime(year, month, 1);

                if (isRentFromValid || isRentToValid || isSpanningMonth)
                    validContracts.Add(contract);
            });

            return validContracts;
        }
    }
}
