using Microsoft.AspNetCore.Mvc;
using RealEst.Core.DTOs;
using RealEst.Services.Services.Interfaces;

namespace RealEst.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthenticationController : ControllerBase
    {
        private readonly IAuthenticationService _authenticationService;

        public AuthenticationController(IAuthenticationService authenticationService)
        {  
            _authenticationService = authenticationService;;
        }

        [HttpPost("Register")]
        public async Task<IActionResult> Register([FromBody] UserRegistrationDto userDto)
        {
            if(await _authenticationService.CheckIfUserExists(userDto))
            {
                return BadRequest("User with provided Email already exists!");
            }

            var result = await _authenticationService.RegisterUser(userDto);

            if(!result)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Creation went wrong!");
            }

            return Ok("User registered successfully!");
        }

        [HttpPost("RegisterOrganisationOwner")]
        public async Task<IActionResult> RegisterOrganisationOwner([FromBody] UserRegistrationDto userDto)
        {
            if (await _authenticationService.CheckIfUserExists(userDto))
            {
                return BadRequest("User with provided Email already exists!");
            }

            var result = await _authenticationService.RegisterAdmin(userDto);

            if (!result)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Creation went wrong!");
            }

            return Ok("Admin registered successfully!");
        }

        [HttpPost("Login")]
        public async Task<IActionResult> Login([FromBody] UserLoginDto userLoginDto)
        {
            var token = await _authenticationService.Login(userLoginDto);

            if(token == null)
            {
                return BadRequest("User doesn't exists!");
            }

            return Ok(token);
        }
    }
}
