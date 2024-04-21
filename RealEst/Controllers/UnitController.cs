using Microsoft.AspNetCore.Mvc;
using RealEst.Core.Models;
using RealEst.Services.Services.Interfaces;

namespace RealEst.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UnitController : ControllerBase
    {
        private readonly IUnitService _unitService;

        public UnitController(IUnitService unitService)
        {
            _unitService = unitService;
        }

        [HttpGet]
        public IActionResult Index()
        {
            var units = _unitService.GetAll();

            if (units == null || units.Count == 0)
                return NotFound("You don't have any units! Please, create one");

            return Ok(units);
        }

        [HttpGet("{id}")]
        public IActionResult GetUnitById(int id)
        {
            var unit = _unitService.GetById(id);

            if (unit == null)
                return NotFound("You entered wrong unit ID!");

            return Ok(unit);
        }

        [HttpPost("create")]
        public IActionResult CreateNewUnit([FromBody] Unit unit)
        {
            if(!_unitService.Add(unit))
                return NotFound("Something went wrong!");
            
            return Created("api/Units", unit);
        }

        [HttpDelete("delete/{id}")]
        public IActionResult DeleteUnit(int id)
        {
            if (!_unitService.DeleteById(id))
                return NotFound("You entered wrong unit ID!");

            return Ok(_unitService.GetAll());
        }

        [HttpPatch("edit/{id}")]
        public IActionResult UpdateUnit(int id, [FromBody] Unit unit)
        {
            if (!_unitService.Update(id, unit))
                return NotFound("You entered wrong unit ID!");

            return Ok(_unitService.GetById(id));
        }
    }
}
