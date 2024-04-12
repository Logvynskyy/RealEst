using Microsoft.AspNetCore.Mvc;
using RealEst.Core.Models;
using RealEst.Services.Service;

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
            if (_unitService.GetAll() == null)
                return NotFound("You don't have any units! Please, create one");

            return Ok(_unitService.GetAll());
        }

        [HttpGet("{id}")]
        public IActionResult GetUnitById(int id)
        {
            if (_unitService.GetById(id) == null)
                return NotFound("You entered wrong unit ID!");

            return Ok(_unitService.GetById(id));
        }

        [HttpPost("create")]
        public IActionResult CreateNewUnit([FromBody] Unit unit)
        {
            if(!_unitService.Add(unit))
                return NotFound("Something went wrong!");
            
            return Created("api/Unit", unit);
        }

        [HttpDelete("delete/{id}")]
        public IActionResult DeleteUnit(int id)
        {
            if (!_unitService.DeleteById(id))
                return NotFound("You entered wrong unit ID!");

            //_unitService.DeleteById(id);

            return Ok(_unitService.GetAll());
        }

        [HttpPatch("edit/{id}")]
        public IActionResult UpdateUnit(int id, [FromBody] Unit unit)
        {
            if (!_unitService.Update(id, unit))
                return NotFound("You entered wrong unit ID!");

            //_unitService.Update(id, unit);

            return Ok(_unitService.GetById(id));
        }
    }
}
