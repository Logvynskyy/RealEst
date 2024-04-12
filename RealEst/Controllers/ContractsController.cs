using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using RealEst.Core.Models;
using RealEst.Services.Service;

namespace RealEst.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ContractsController : ControllerBase
    {
        private readonly IContractService _contractService;

        public ContractsController(IContractService contractService)
        {
            _contractService = contractService;
        }

        [HttpGet]
        public IActionResult Index()
        {
            if (_contractService.GetAll() == null)
                return NotFound("You don't have any contracts! Please, create one");

            return Ok(_contractService.GetAll());
        }

        [HttpGet("{id}")]
        public IActionResult GetContractById(int id)
        {
            if (_contractService.GetById(id) == null)
                return NotFound("You entered wrong contract ID!");

            return Ok(_contractService.GetById(id));
        }

        [HttpPost("create")]
        public IActionResult CreateNewContract([FromBody] Contract contract)
        {
            if (!_contractService.Add(contract))
                return NotFound("Something went wrong!");

            return Created("api/Unit", contract);
        }

        [HttpDelete("delete/{id}")]
        public IActionResult DeleteContract(int id)
        {
            if (!_contractService.DeleteById(id))
                return NotFound("You entered wrong contract ID!");

            return Ok(_contractService.GetAll());
        }

        [HttpPatch("edit/{id}")]
        public IActionResult UpdateContract(int id, [FromBody] Contract contract)
        {
            if (!_contractService.Update(id, contract))
                return NotFound("You entered wrong contract ID!");

            return Ok(_contractService.GetById(id));
        }
    }
}
