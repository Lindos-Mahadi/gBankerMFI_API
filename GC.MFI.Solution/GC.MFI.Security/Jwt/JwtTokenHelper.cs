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
using GC.MFI.Models.Modules.Distributions.Security;
using Microsoft.AspNetCore.Identity;
using GC.MFI.Services.Modules.GcMfi.Interfaces;
using System.Data;
using Microsoft.Extensions.Caching.Memory;
using Microsoft.AspNetCore.Http;
using System.Linq;
using Microsoft.Extensions.Configuration;
using Microsoft.Data.SqlClient;

namespace GC.MFI.Security.Jwt
{
    public class JwtTokenHelper : IJwtTokenHelper
    {
        private readonly IJWT jwt;
        private readonly IAuthenticationService _authenticationService;
        private UserManager<ApplicationUser> _userManager;
        private readonly IMemberService memberService;
        private readonly IMemoryCache memoryCache;
        private readonly IHttpContextAccessor httpContextAccessor;
        private readonly string _connectionString;

        public JwtTokenHelper(IJWT jwt, IConfiguration configuration, IHttpContextAccessor httpContextAccessor, IAuthenticationService authenticationService, IMemoryCache memoryCache, IMemberService memberService, UserManager<ApplicationUser> userManager)
        {
            this.jwt = jwt;
            this._authenticationService = authenticationService;
            this._userManager = userManager;
            this.memberService = memberService;
            this.memoryCache = memoryCache;
            this.httpContextAccessor = httpContextAccessor;
            _connectionString = configuration.GetConnectionString("DefaultConnection");

        }
        public Tokens GenerateRefreshToken(string userName)
        {
            throw new NotImplementedException();
        }

        public async  Task<Tokens> Authenticate(AuthenticationModel user)
        {
            var userModel = await  _authenticationService.Authenticate(user.UserId, user.Password);
            if (userModel == null)
            {
                return null;
            }
            // Else we generate JSON Web Token
            var tokenHandler = new JwtSecurityTokenHandler();
            var tokenKey = Encoding.UTF8.GetBytes(jwt.SecretKey);
            //var roles = JsonConvert.SerializeObject("Admin");
            var permissions = new List<UserPermissionSet>();
            var roles = await _userManager.GetRolesAsync(userModel);
            var claims = new List<Claim>();
            //add static claims
            claims.Add(new Claim("email", userModel.Email));
            claims.Add(new Claim("userName", userModel.UserName));
            claims.Add(new Claim("id", userModel.Id));
            claims.Add(new Claim("fullName", userModel.FirstName +" " +( userModel.LastName != null ? userModel.LastName : "" ) ));
            claims.Add(new Claim("IsTemporaryPassword", userModel.IsTemporaryPassword.ToString())); 
            if(userModel.PortalMemberID != null)
            {
                long id = (long)userModel.PortalMemberID;
                claims.Add(new Claim("PortalMemberId", id.ToString()));
                var Member = await memberService.GetMemberByPortalId(id);
                if(Member != null)
                {
                    claims.Add(new Claim("memberId", Member.MemberID.ToString()));
                    claims.Add(new Claim("centerId", Member.CenterID.ToString()));
                    claims.Add(new Claim("orgId", Member.OrgID.ToString()));
                    claims.Add(new Claim("officeId", Member.OfficeID.ToString()));
                    claims.Add(new Claim("memberStatus", Member.MemberStatus.ToString()));
                    claims.Add(new Claim("BanglaName", Member.BanglaName != null ? Member.BanglaName : ""));
                    int status = Int32.Parse(Member.MemberStatus);
                    if (status > 0)
                    {
                        if (roles != null)
                        {
                            foreach (var role in roles)
                            {
                                claims.Add(new Claim(ClaimTypes.Role, role));
                            }

                        };
                    }else
                    {
                        claims.Add(new Claim(ClaimTypes.Role, "UnActive"));
                        claims.Add(new Claim("memberStatus", "0"));
                    }
                }else
                {
                    claims.Add(new Claim(ClaimTypes.Role, "UnActive"));
                    claims.Add(new Claim("memberStatus", "0"));
                }
            }else
            {
                claims.Add(new Claim(ClaimTypes.Role, "UnActive"));
                claims.Add(new Claim("memberStatus", "0"));
            }


            var tokenDescriptor = new SecurityTokenDescriptor
            {
             Subject = new ClaimsIdentity(claims),
            Expires = DateTime.UtcNow.AddMinutes(60),
            SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(tokenKey), SecurityAlgorithms.HmacSha256Signature)
            };

            
            var token = tokenHandler.CreateToken(tokenDescriptor);
            var acctoken = tokenHandler.WriteToken(token);
            var cacheEntryOptions = new MemoryCacheEntryOptions().SetAbsoluteExpiration(TimeSpan.FromMinutes(60));
            memoryCache.Set("useridentifier", acctoken, cacheEntryOptions);
            SqlDependency.Start(_connectionString);

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
