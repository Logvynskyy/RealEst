using Microsoft.Extensions.Logging;
using RealEst.Core.DTOs;
using RealEst.Core.Models;
using RealEst.DataAccess.Interfaces;
using RealEst.Services.Services.Interfaces;

namespace RealEst.Services.Services.Implementations
{
    public class TennantService : ITennantService
    {
        private readonly ITennantRepository _tennantRepository;
        private readonly ILogger<TennantService> _logger;
        private readonly IAuthenticationService _authenticationService;

        public TennantService(ITennantRepository tennantRepository, 
            ILogger<TennantService> logger, 
            IAuthenticationService authenticationService)
        {
            _tennantRepository = tennantRepository;
            _logger = logger;
            _authenticationService = authenticationService;
        }

        public bool Add(TennantDto tennant)
        {
            try
            {
                _tennantRepository.Add(DtoToEntity(tennant));

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

        public List<TennantDto> GetAll()
        {
            try
            {
                _logger.LogInformation("Returned all tennants");
                return _tennantRepository.GetAll().Select(x => EntityToDto(x)).ToList();
            }
            catch (NullReferenceException e)
            {
                _logger.LogError(e.Message);
                return null;
            }
        }

        public TennantDto GetById(int id)
        {
            try
            {
                _logger.LogInformation("Getting tennant with id {0}", id);
                return EntityToDto(_tennantRepository.GetById(id));
            }
            catch (ArgumentOutOfRangeException e)
            {
                _logger.LogError(e.Message);
                return null;
            }
        }

        public bool Update(int id, TennantDto tennantDto)
        {
            try
            {
                // TODO: Add validation
                var tennant = DtoToEntity(tennantDto);

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

        public Tennant DtoToEntity(TennantDto tennantDto)
        {
            return new Tennant(tennantDto, _authenticationService.GetCurrentOrganisation());
        }

        public TennantDto EntityToDto(Tennant entity)
        {
            return new TennantDto
            {
                Id = entity.Id,
                Name = entity.Name,
                LastName = entity.LastName,
                Email = entity.Email,
                Debt = entity.Debt
            };
        }
    }
}
