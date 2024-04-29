using RealEst.Core.DTOs;
using RealEst.DataAccess;

namespace RealEst.Services.Services.Interfaces
{
    public interface IAuthenticationService
    {
        Task<bool> CheckIfUserExists(UserDto userDto);
        Task<bool> RegisterUser(UserDto userDtoDto);
        Task<bool> RegisterAdmin(UserDto userDtoDto);
        Task<string> Login(UserDto userDto);
        Task<ApplicationUser> GetCurrentUser(UserDto userDto);
    }
}
