using Microsoft.AspNetCore.Identity;
using System.Threading.Tasks;

namespace GC.MFI.Services.Modules.Security.Interfaces
{
    public interface IAuthenticationService
    {       
         IdentityUser Authenticate(string username, string password);

    }
}
