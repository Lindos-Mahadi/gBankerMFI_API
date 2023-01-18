using Microsoft.IdentityModel.Tokens;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using GC.MFI.Security.Models;
using GC.MFI.Services.Modules.Security.Interfaces;
using System.Threading.Tasks;
using GC.MFI.Models;

namespace GC.MFI.Security.Jwt
{
    public class JwtTokenHelper : IJwtTokenHelper
    {
        private readonly IJWT jwt;
        private readonly IAuthenticationService _authenticationService;

        public JwtTokenHelper(IJWT jwt, IAuthenticationService authenticationService)
        {
            this.jwt = jwt;
            this._authenticationService = authenticationService;
        }
        public Tokens GenerateRefreshToken(string userName)
        {
            throw new NotImplementedException();
        }

        public  Tokens Authenticate(AuthenticationModel user, AzureAD connection)
        {
            var userModel =  _authenticationService.Authenticate(user.UserId, user.Password);
            if (userModel == null)
            {
                return null;
            }
            // Else we generate JSON Web Token
            var tokenHandler = new JwtSecurityTokenHandler();
            var tokenKey = Encoding.UTF8.GetBytes(jwt.SecretKey);
            var roles = JsonConvert.SerializeObject("Admin");
            var permissions = new List<UserPermissionSet>();
            //if (userModel.Roles != null)
            //{
            //    foreach (var role in userModel.Roles)
            //    {
            //        var currentRolePermisions = _roleservice.FindById(role.ID);
            //        var rolepermissions = currentRolePermisions.Permissions;
            //        if (rolepermissions != null)
            //        {
            //            var userPermissions = rolepermissions.Select(s => new UserPermissionSet { RoleId = role.ID, RoleName = role.Name, PermissionSets = s.PermissionSets, PermissionDetail = s.PermissionId.ToEntityAsync().GetAwaiter().GetResult() });
            //            permissions.AddRange(userPermissions);
            //        }
            //    }
            //}
            var permissioJson = JsonConvert.SerializeObject(permissions);
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new Claim[] {
                new Claim("fullName", $"{userModel.UserName}"),
                new Claim("email", userModel.Email),
                new Claim("userName", userModel.UserName),
                new Claim("id", userModel.Id),
                new Claim("permissionsJson", permissioJson),
                new Claim(ClaimTypes.Role, "Administrator")
              }),
                Expires = DateTime.UtcNow.AddMinutes(480),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(tokenKey), SecurityAlgorithms.HmacSha256Signature)
            };
            var token = tokenHandler.CreateToken(tokenDescriptor);
            return new Tokens { AccessToken = tokenHandler.WriteToken(token) };
        }
        public ClaimsPrincipal GetPrincipalFromExpiredToken(string token)
        {
            throw new NotImplementedException();
        }

        ClaimsPrincipal IAuthenticationProvider.GetPrincipalFromExpiredToken(string token)
        {
            throw new NotImplementedException();
        }
    }

    public class UserPermissionSet
    {
        public string RoleId { get; set; }
        public string RoleName { get; set; }
        //public PermissionModel PermissionDetail { get; set; }
        public List<string> PermissionSets { get; set; }
    }
}
