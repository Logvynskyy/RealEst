using Microsoft.Extensions.Logging;
using RealEst.Core.DTOs;
using RealEst.Core.Models;
using RealEst.DataAccess.Interfaces;
using RealEst.Services.Services.Interfaces;
using RealEst.Services.Validators.Interfaces;

namespace RealEst.Services.Services.Implementations
{
    public class UnitService : IUnitService
    {
        private readonly IUnitRepository _unitRepository;
        //private readonly IUnitValidator _unitValidator;
        private readonly ILogger<UnitService> _logger;
        private readonly IAuthenticationService _authenticationService;
        private readonly Organisation _currentOrganisation;

        public UnitService(IUnitRepository unitRepository, 
            ILogger<UnitService> logger, 
            IAuthenticationService authenticationService
            //IUnitValidator unitValidator
            )
        {
            _unitRepository = unitRepository;
            _logger = logger;
            _authenticationService = authenticationService;
            //_unitValidator = unitValidator;
            _currentOrganisation = _authenticationService.GetCurrentOrganisation();
        }

        public bool Add(UnitDto unitDto)
        {
            try
            {
                //if (!_unitValidator.Validate(unitDto))
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

                DeleteDefectsOnUnitById(id);
                return _unitRepository.DeleteById(id);
            }
            catch (ArgumentOutOfRangeException e)
            {
                _logger.LogError(e.Message);
                return false;
            }
        }

        public List<UnitOutputDto> GetAll()
        {
            try
            {
                _logger.LogInformation("Returned all units");

                return _unitRepository.GetAll()
                    .Where(u => u.Organisation == _currentOrganisation)
                    .Select(x => EntityToDto(x)).ToList();
            }
            catch (NullReferenceException e)
            {
                _logger.LogError(e.Message);
                return null;
            }
        }

        public UnitOutputDto GetById(int id)
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
            return new Unit(DtoToUnitDefectsDto(unitDto), _authenticationService.GetCurrentOrganisation());
        }

        public UnitOutputDto EntityToDto(Unit unit)
        {
            return new UnitOutputDto
            {
                Id = unit.Id,
                Name = unit.Name,
                Address = unit.Address,
                UnitType = unit.UnitType,
                Footage = unit.Footage,
                Defects = DefectsEntityToDto(unit.Defects!)
            };
        }

        private void DeleteDefectsOnUnitById(int unitId)
        {
            var unit = _unitRepository.GetById(unitId);
            if (unit != null)
            {
                unit.Defects = null;
                _unitRepository.Update(unitId, unit);
            }
        }

        private UnitDefectsDto DtoToUnitDefectsDto(UnitDto unitDto)
        {
            return unitDto.Defects == null
                ? new UnitDefectsDto
                {
                    Id = unitDto.Id,
                    Name = unitDto.Name,
                    Address = unitDto.Address,
                    UnitType = unitDto.UnitType,
                    Footage = unitDto.Footage
                }
                : new UnitDefectsDto
                {
                    Id = unitDto.Id,
                    Name = unitDto.Name,
                    Address = unitDto.Address,
                    UnitType = unitDto.UnitType,
                    Footage = unitDto.Footage,
                    Defects = unitDto.Defects
                        .Select(defectDto => new Defect(defectDto, _authenticationService.GetCurrentOrganisation()))
                        .ToList()
                };

        }

        private List<DefectOutputDto> DefectsEntityToDto(IList<Defect> defects)
        {
            return defects == null ? new List<DefectOutputDto>() : 
                defects.Select(defect => new DefectOutputDto
                {
                    Id = defect.Id,
                    Name = defect.Name,
                    Description = defect.Description,
                    DefectType = defect.DefectType
                }).ToList();
        }
    }
}
