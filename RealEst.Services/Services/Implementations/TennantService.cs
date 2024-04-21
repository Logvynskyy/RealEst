using Microsoft.Extensions.Logging;
using RealEst.Core.Models;
using RealEst.DataAccess.Interfaces;
using RealEst.Services.Services.Interfaces;

namespace RealEst.Services.Services.Implementations
{
    public class TennantService : ITennantService
    {
        private readonly ITennantRepository _tennantRepository;
        private readonly ILogger<TennantService> _logger;

        public TennantService(ITennantRepository tennantRepository, ILogger<TennantService> logger)
        {
            _tennantRepository = tennantRepository;
            _logger = logger;
        }
        public bool Add(Tennant tennant)
        {
            try
            {
                _tennantRepository.Add(tennant);

                _logger.LogInformation("Added new tennant");
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
                _logger.LogInformation("Deleting tennant with id {0}", id);
                return _tennantRepository.DeleteById(id);
            }
            catch (ArgumentOutOfRangeException e)
            {
                _logger.LogError(e.Message);
                return false;
            }
        }

        public List<Tennant> GetAll()
        {
            try
            {
                _logger.LogInformation("Returned all tennants");
                return _tennantRepository.GetAll();
            }
            catch (NullReferenceException e)
            {
                _logger.LogError(e.Message);
                return null;
            }
        }

        public Tennant GetById(int id)
        {
            try
            {
                _logger.LogInformation("Getting tennant with id {0}", id);
                return _tennantRepository.GetById(id);
            }
            catch (ArgumentOutOfRangeException e)
            {
                _logger.LogError(e.Message);
                return null;
            }
        }

        public bool Update(int id, Tennant tennant)
        {
            try
            {
                // TODO: Add validation
                tennant.Id = id;

                _tennantRepository.Update(id, tennant);

                _logger.LogInformation("Updating tennant with id {0}", id);
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
