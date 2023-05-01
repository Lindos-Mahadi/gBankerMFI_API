using GC.MFI.Models.ViewModels;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GC.MFI.Utility.Helpers
{
    public static class JwtTokenDecode
    {
        public static JwtTokenModel GetDetailsFromToken (string jwtToken)
        {
            var handler = new JwtSecurityTokenHandler();
            var jwtSecurityToken = handler.ReadJwtToken(jwtToken);
            var userName = jwtSecurityToken.Claims.First(t => t.Type == "userName").Value;
            var userId = jwtSecurityToken.Claims.First(t => t.Type == "id").Value;
            var email = jwtSecurityToken.Claims.First(t => t.Type == "email").Value;
            var memberID = jwtSecurityToken.Claims.First(t => t.Type == "memberId").Value;
            var officeID = jwtSecurityToken.Claims.First(t => t.Type == "officeId").Value;
            var PortalMemberId = long.Parse(jwtSecurityToken.Claims.First(t => t.Type == "PortalMemberId")?.Value);
            
            return new JwtTokenModel { UserName = userName, UserId = userId, UserEmail = email, PortalMemberId = PortalMemberId, MemberID = memberID, OfficeId = officeID };
        }
    }
}
