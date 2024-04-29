using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using RealEst.Core.Constants;
using RealEst.Core.DTOs;
using RealEst.DataAccess;
using RealEst.DataAccess.Interfaces;
using RealEst.Services.Services.Interfaces;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace RealEst.Services.Services.Implementations
{
    public class AuthenticationService : IAuthenticationService
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly IOrganisationRepository _organisationRepository;
        private readonly IConfiguration _config;

        public AuthenticationService(UserManager<ApplicationUser> userManager, RoleManager<IdentityRole> roleManager, 
            IOrganisationRepository organisationRepository,
            IConfiguration configuration) 
        {
            _userManager = userManager;
            _roleManager = roleManager;
            _organisationRepository = organisationRepository;
            _config = configuration;
        }

        public async Task<bool> CheckIfUserExists(UserRegistrationDto userDto)
        {
            return await _userManager.FindByNameAsync(userDto.Username) != null;
        }

        public async Task<bool> CheckIfUserExists(UserLoginDto userLoginDto)
        {
            return await _userManager.FindByNameAsync(userLoginDto.Username) != null;
        }

        public async Task<ApplicationUser> GetCurrentUser(UserRegistrationDto userDto)
        {
            return await _userManager.FindByNameAsync(userDto.Username);
        }

        public async Task<ApplicationUser> GetCurrentUser(UserLoginDto userLoginDto)
        {
            return await _userManager.FindByNameAsync(userLoginDto.Username);
        }

        public async Task<string> Login(UserLoginDto userLoginDto)
        {
            var user = GetCurrentUser(userLoginDto).Result;

            if (!CheckIfUserExists(userLoginDto).Result || !await _userManager.CheckPasswordAsync(user, userLoginDto.Password))
            {
                return null;
            }

            var userRoles = await _userManager.GetRolesAsync(user);

            var authClaims = new List<Claim>
                {
                    new Claim(ClaimTypes.Name, user.UserName),
                    new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                };

            foreach (var role in userRoles)
            {
                authClaims.Add(new Claim(ClaimTypes.Role, role));
            }

            var authSignInKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_config["JwtSettings:Key"]!));

            var token = new JwtSecurityToken(
                issuer: _config["JwtSettings:Issuer"],
                audience: _config["JwtSettings:Audience"],
                expires: DateTime.Now.AddHours(3),
                claims: authClaims,
                signingCredentials: new SigningCredentials(authSignInKey, SecurityAlgorithms.HmacSha256)
            );

            return new JwtSecurityTokenHandler().WriteToken(token);
        }

        public async Task<bool> RegisterUser(UserRegistrationDto userDto)
        {
            var user = new ApplicationUser
            {
                UserName = userDto.Username,
                SecurityStamp = Guid.NewGuid().ToString(),
                Organisation = _organisationRepository.GetById(userDto.OrganisationId)
            };

            var result = await _userManager.CreateAsync(user, userDto.Password);

            return result.Succeeded;
        }

        public async Task<bool> RegisterAdmin(UserRegistrationDto userDto)
        {
            var user = new ApplicationUser
            {
                UserName = userDto.Username,
                SecurityStamp = Guid.NewGuid().ToString(),
                Organisation = _organisationRepository.GetById(userDto.OrganisationId)
            };

            var result = await _userManager.CreateAsync(user, userDto.Password);

            await EnsureRolesExist();
            
            await _userManager.AddToRoleAsync(user, UserRoles.Admin);

            return result.Succeeded;
        }

        private async Task EnsureRolesExist()
        {
            if(!await _roleManager.RoleExistsAsync(UserRoles.User))
            {
                await _roleManager.CreateAsync(new IdentityRole(UserRoles.User));
            }

            if (!await _roleManager.RoleExistsAsync(UserRoles.Admin))
            {
                await _roleManager.CreateAsync(new IdentityRole(UserRoles.Admin));
            }

            return;
        }
    }
}
