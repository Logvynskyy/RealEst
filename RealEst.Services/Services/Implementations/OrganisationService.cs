using Microsoft.Extensions.Logging;
using RealEst.Core.Models;
using RealEst.DataAccess.Interfaces;
using RealEst.Services.Services.Interfaces;

namespace RealEst.Services.Services.Implementations
{
    public class OrganisationService : IOrganisationService
    {
        private readonly IOrganisationRepository _organisationRepository;
        private readonly ILogger<OrganisationService> _logger;

        public OrganisationService(IOrganisationRepository organisationRepository, ILogger<OrganisationService> logger)
        {
            _organisationRepository = organisationRepository;
            _logger = logger;
        }

        public bool Add(Organisation organisation)
        {
            try
            {
                _organisationRepository.Add(organisation);

                _logger.LogInformation("Added new organisation");
                return true;
            }
            catch (InvalidOperationException e)
            {
                _logger.LogError(e.Message);
                return false;
            }
        }

        public Organisation GetById(int id)
        {
            try
            {
                _logger.LogInformation("Getting organisation with id {0}", id);
                return _organisationRepository.GetById(id);
            }
            catch (ArgumentOutOfRangeException e)
            {
                _logger.LogError(e.Message);
                return null;
            }
        }
    }
}
