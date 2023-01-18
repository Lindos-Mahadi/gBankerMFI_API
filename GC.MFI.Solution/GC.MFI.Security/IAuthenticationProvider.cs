using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using GC.MFI.Models;
using GC.MFI.Security.Models;

namespace GC.MFI.Security
{
    public interface IAuthenticationProvider
    {
        Tokens Authenticate(AuthenticationModel user);
        Tokens GenerateRefreshToken(string userName);
        ClaimsPrincipal GetPrincipalFromExpiredToken(string token);
    }
}
