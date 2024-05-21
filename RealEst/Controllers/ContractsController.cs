using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using RealEst.Core.DTOs;
using RealEst.Services.Services.Interfaces;

namespace RealEst.Controllers
{
    [Authorize(AuthenticationSchemes = "Bearer")]
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
            var contracts = _contractService.GetAll();

            if (contracts == null || contracts.Count == 0)
                return NotFound("You don't have any contracts! Please, create one");

            return Ok(contracts);
        }

        [HttpGet("{id}")]
        public IActionResult GetContractById(int id)
        {
            var contract = _contractService.GetById(id);

            if (contract == null)
                return NotFound("You entered wrong contract ID!");

            return Ok(contract);
        }

        [HttpPost("create")]
        public IActionResult CreateNewContract([FromBody] ContractInputDto contract)
        {
            if (!_contractService.Add(contract))
                return NotFound("Something went wrong!");

            return Created("api/Contracts", null);
        }

        [HttpDelete("delete/{id}")]
        public IActionResult DeleteContract(int id)
        {
            if (!_contractService.DeleteById(id))
                return NotFound("You entered wrong contract ID!");

            return Ok(_contractService.GetAll());
        }

        [HttpPatch("edit/{id}")]
        public IActionResult UpdateContract(int id, [FromBody] ContractInputDto contract)
        {
            if (!_contractService.Update(id, contract))
                return NotFound("You entered wrong contract ID!");

            return Ok(_contractService.GetById(id));
        }
    }
}
