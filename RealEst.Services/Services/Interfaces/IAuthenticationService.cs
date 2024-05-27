using RealEst.Core.DTOs;
using RealEst.Core.Models;
using RealEst.DataAccess;

namespace RealEst.Services.Services.Interfaces
{
    public interface IAuthenticationService
    {
        Task<bool> CheckIfUserExists(UserRegistrationDto userDto);
        Task<bool> RegisterUser(UserRegistrationDto userDto);
        Task<bool> RegisterAdmin(UserRegistrationDto userDto);
        List<UserLoginDto> GetUsers();
        Task<bool> DeleteByUsername(string username);
        Task<LoginDto> Login(UserLoginDto userLoginDto);
        Task<ApplicationUser> GetCurrentUser(UserRegistrationDto userDto);
        Organisation GetCurrentOrganisation();
    }
}
