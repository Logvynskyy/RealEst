using Microsoft.Extensions.Logging;
using RealEst.Core.DTOs;
using RealEst.Core.Models;
using RealEst.DataAccess.Interfaces;
using RealEst.Services.Services.Interfaces;

namespace RealEst.Services.Services.Implementations
{
    public class UnitService : IUnitService
    {
        private readonly IUnitRepository _unitRepository;
        private readonly ILogger<UnitService> _logger;
        private readonly IAuthenticationService _authenticationService;

        public UnitService(IUnitRepository unitRepository, 
            ILogger<UnitService> logger, 
            IAuthenticationService authenticationService)
        {
            _unitRepository = unitRepository;
            _logger = logger;
            _authenticationService = authenticationService;
        }

        public bool Add(UnitDto unitDto)
        {
            try
            {
                //if (!_unitValidator.Validate(unit).FirstOrDefault())
                //    throw new InvalidOperationException("You passed invalid unit!");
                var unit = DtoToEntity(unitDto);

                _unitRepository.Add(unit);

                _logger.LogInformation("Added new unit");
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
                _logger.LogInformation("Deleting unit with id {0}", id);
                return _unitRepository.DeleteById(id);
            }
            catch (ArgumentOutOfRangeException e)
            {
                _logger.LogError(e.Message);
                return false;
            }
        }

        public List<UnitDto> GetAll()
        {
            try
            {
                _logger.LogInformation("Returned all units");

                return _unitRepository.GetAll().Select(x => EntityToDto(x)).ToList();
            }
            catch (NullReferenceException e)
            {
                _logger.LogError(e.Message);
                return null;
            }
        }

        public UnitDto GetById(int id)
        {
            try
            {
                _logger.LogInformation("Getting unit with id {0}", id);

                return EntityToDto(_unitRepository.GetById(id));
            }
            catch (ArgumentOutOfRangeException e)
            {
                _logger.LogError(e.Message);
                return null;
            }
        }

        public bool Update(int id, UnitDto unitDto)
        {
            try
            {
                // TODO: Add validation
               var unit = DtoToEntity(unitDto);

                _unitRepository.Update(id, unit);

                _logger.LogInformation("Updating unit with id {0}", id);
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

        public Unit DtoToEntity(UnitDto unitDto)
        {
            return new Unit(unitDto, _authenticationService.GetCurrentOrganisation());
        }

        public UnitDto EntityToDto(Unit unit)
        {
            return new UnitDto
            {
                Id = unit.Id,
                Name = unit.Name,
                Address = unit.Address,
                UnitType = unit.UnitType,
                Footage = unit.Footage,
                Defects = unit.Defects
            };
        }
    }
}
