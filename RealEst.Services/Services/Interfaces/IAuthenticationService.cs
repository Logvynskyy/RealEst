using RealEst.Core.DTOs;
using RealEst.DataAccess;

namespace RealEst.Services.Services.Interfaces
{
    public interface IAuthenticationService
    {
        Task<bool> CheckIfUserExists(UserRegistrationDto userDto);
        Task<bool> RegisterUser(UserRegistrationDto userDto);
        Task<bool> RegisterAdmin(UserRegistrationDto userDto);
        Task<string> Login(UserLoginDto userLoginDto);
        Task<ApplicationUser> GetCurrentUser(UserRegistrationDto userDto);
    }
}
