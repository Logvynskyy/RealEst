using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using RealEst.Core.Constants;
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
            _authenticationService = authenticationService;
        }

        [Authorize(AuthenticationSchemes = "Bearer", Roles = UserRoles.Admin)]
        [HttpGet("Users")]
        public IActionResult GetUsers()
        {
            var users = _authenticationService.GetUsers();

            if (users == null || users.Count == 0)
                return NotFound("You don't have any users! Please, create one");

            return Ok(users);
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

            return Ok(new {
                message = "User registered successfully!"
            });
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

            return Ok(new {
                message = "Admin registered successfully!"
            });
        }

        [HttpPost("Login")]
        public async Task<IActionResult> Login([FromBody] UserLoginDto userLoginDto)
        {
            var loginInfo = await _authenticationService.Login(userLoginDto);

            if(loginInfo == null)
            {
                return Unauthorized("User doesn't exists!");
            }

            if(loginInfo.Token == "pass")
            {
                return Unauthorized("You've entered the wrong password!");
            }

            return Ok(loginInfo);
        }

        [Authorize(AuthenticationSchemes = "Bearer", Roles = UserRoles.Admin)]
        [HttpDelete("Users/delete/{username}")]
        public async Task<IActionResult> DeleteUser(string username)
        {
            var result = await _authenticationService.DeleteByUsername(username);

            if (!result)
                return NotFound("You entered wrong contact ID!");

            return Ok(_authenticationService.GetUsers());
        }
    }
}
