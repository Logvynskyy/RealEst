using Microsoft.Extensions.Logging;
using RealEst.Core.Models;
using RealEst.DataAccess.Interfaces;
using RealEst.Services.Services.Interfaces;

namespace RealEst.Services.Services.Implementations
{
    public class UnitService : IUnitService
    {
        private readonly IUnitRepository _unitRepository;
        private readonly ILogger<UnitService> _logger;

        public UnitService(IUnitRepository unitRepository, ILogger<UnitService> logger)
        {
            _unitRepository = unitRepository;
            _logger = logger;
        }

        public bool Add(Unit unit)
        {
            try
            {
                //if (!_unitValidator.Validate(unit).FirstOrDefault())
                //    throw new InvalidOperationException("You passed invalid unit!");

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

        public List<Unit> GetAll()
        {
            try
            {
                _logger.LogInformation("Returned all units");
                return _unitRepository.GetAll();
            }
            catch (NullReferenceException e)
            {
                _logger.LogError(e.Message);
                return null;
            }
        }

        public Unit GetById(int id)
        {
            try
            {
                _logger.LogInformation("Getting unit with id {0}", id);
                return _unitRepository.GetById(id);
            }
            catch (ArgumentOutOfRangeException e)
            {
                _logger.LogError(e.Message);
                return null;
            }
        }

        public bool Update(int id, Unit unit)
        {
            try
            {
                // TODO: Add validation
                unit.Id = id;

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
    }
}
