using Microsoft.AspNetCore.Identity;
using RealEst.Core.Models;

namespace RealEst.DataAccess
{
    public class ApplicationUser : IdentityUser
    {
        public Organisation Organisation { get; set; } = null!;
    }
}
