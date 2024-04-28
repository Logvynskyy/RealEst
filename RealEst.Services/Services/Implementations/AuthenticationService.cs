using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using RealEst.Core.DTOs;
using RealEst.DataAccess;
using RealEst.Services.Services.Interfaces;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace RealEst.Services.Services.Implementations
{
    public class AuthenticationService : IAuthenticationService
    {
        private readonly UserManager<ApplicationUser> _userManager;
        //private readonly RoleManager<ApplicationUser> _roleManager; RoleManager<ApplicationUser> roleManager
        private readonly IConfiguration _config;

        public AuthenticationService(UserManager<ApplicationUser> userManager, IConfiguration configuration) {
            _userManager = userManager;
            //_roleManager = roleManager;
            _config = configuration;
        }

        public async Task<bool> CheckIfUserExists(UserDto userDto)
        {
            return await _userManager.FindByNameAsync(userDto.Username) != null;
        }

        public async Task<ApplicationUser> GetCurrentUser(UserDto userDto)
        {
            return await _userManager.FindByNameAsync(userDto.Username);
        }

        public async Task<string> Login(UserDto userDto)
        {
            var user = GetCurrentUser(userDto).Result;

            if (!CheckIfUserExists(userDto).Result || !await _userManager.CheckPasswordAsync(user, userDto.Password))
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

        public async Task<bool> RegisterUser(UserDto registrationDto)
        {
            var user = new ApplicationUser
            {
                UserName = registrationDto.Username,
                SecurityStamp = Guid.NewGuid().ToString()
            };

            var result = await _userManager.CreateAsync(user, registrationDto.Password);

            return result.Succeeded;
        }
    }
}
