using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using RealEst.Core.DTOs;
using RealEst.Services.Services.Interfaces;

namespace RealEst.Controllers
{
    [Authorize(AuthenticationSchemes = "Bearer")]
    [Route("api/[controller]")]
    [ApiController]
    public class TennantsController : ControllerBase
    {
        private readonly ITennantService _tennantService;

        public TennantsController(ITennantService tennantService)
        {
            _tennantService = tennantService;
        }

        [HttpGet]
        public IActionResult Index()
        {
            var tennants = _tennantService.GetAll();

            if (tennants == null || tennants.Count == 0)
                return NotFound("You don't have any tennants! Please, create one");

            return Ok(tennants);
        }

        [HttpGet("{id}")]
        public IActionResult GetTennantById(int id)
        {
            var tennant = _tennantService.GetById(id);

            if (tennant == null)
                return NotFound("You entered wrong tennant ID!");

            return Ok(tennant);
        }

        [HttpPost("create")]
        public IActionResult CreateNewTennant([FromBody] TennantDto tennant)
        {
            if (!_tennantService.Add(tennant))
                return NotFound("Something went wrong!");

            return Created("api/Tennants", tennant);
        }

        [HttpDelete("delete/{id}")]
        public IActionResult DeleteTennant(int id)
        {
            if (!_tennantService.DeleteById(id))
                return NotFound("You entered wrong tennant ID!");

            return Ok(_tennantService.GetAll());
        }

        [HttpPatch("edit/{id}")]
        public IActionResult UpdateTennant(int id, [FromBody] TennantDto tennant)
        {
            if (!_tennantService.Update(id, tennant))
                return NotFound("You entered wrong tennant ID!");

            return Ok(_tennantService.GetById(id));
        }
    }
}
