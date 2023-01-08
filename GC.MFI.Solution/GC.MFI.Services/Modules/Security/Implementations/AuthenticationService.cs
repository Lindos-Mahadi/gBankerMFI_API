using GC.MFI.Services.Modules.Security.Interfaces;
using Microsoft.AspNetCore.Identity;
using System.Linq;
using System.Threading.Tasks;

namespace GC.MFI.Services.Modules.Security.Implementations
{
    public class AuthenticationService : IAuthenticationService
    {
        private UserManager<IdentityUser> UserManager;
        public AuthenticationService(UserManager<IdentityUser> _userManager)
        {
            this.UserManager = _userManager;
        }
        public IdentityUser Authenticate(string username, string password)
        {
            var identity = new IdentityUser();
            identity = UserManager.Users.Where(u => u.UserName == username).FirstOrDefault();
            var user = UserManager.FindByNameAsync(username);
            var isValidPassword = false;
            if (identity == null)
                identity = null;
            if (identity != null)
                isValidPassword = UserManager.CheckPasswordAsync(identity, password).GetAwaiter().GetResult();
            if (!isValidPassword)
                identity = null;
            return identity;
        }
    }
}
