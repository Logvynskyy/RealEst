using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using RealEst.Core.Models;
using RealEst.Services.Services.Implementations;
using RealEst.Services.Services.Interfaces;

namespace RealEst.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrganisationsController : ControllerBase
    {
        private readonly IOrganisationService _organisationService;

        public OrganisationsController(IOrganisationService organisationService)
        {
            _organisationService = organisationService;
        }

        [HttpGet("{id}")]
        public IActionResult GetOrganisationById(int id)
        {
            var organisation = _organisationService.GetById(id);

            if (organisation == null)
                return NotFound("You entered wrong organisation ID!");

            return Ok(organisation);
        }

        [HttpPost("create")]
        public IActionResult CreateNewOrganisation([FromBody] Organisation organisation)
        {
            if (!_organisationService.Add(organisation))
                return NotFound("Something went wrong!");

            return Created("api/Organisations", organisation);
        }
    }
}
