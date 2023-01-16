using GC.MFI.DataAccess.Repository.Interfaces;
using GC.MFI.Models;
using GC.MFI.Models.DbModels;
using GC.MFI.Models.Modules.Distributions.Security;
using GC.MFI.Models.Modules.Security;
using GC.MFI.Services.Modules.Security.Interfaces;
using Microsoft.AspNetCore.Identity;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace GC.MFI.Services.Modules.Security.Implementations
{
    public class AuthenticationService : IAuthenticationService
    {
        private UserManager<ApplicationUser> UserManager;
        private IPortalMemberRepository _repository;

        public AuthenticationService(UserManager<ApplicationUser> _userManager, IPortalMemberRepository repository)
        {
            this.UserManager = _userManager;
            this._repository = repository;
        }
        public ApplicationUser Authenticate(string username, string password)
        {
            var identity = new ApplicationUser();
            identity = UserManager.Users.Where(u => u.UserName == username).FirstOrDefault();
          
            var isValidPassword = false;
            if (identity == null)
                identity = null;
            if (identity != null)
                isValidPassword = UserManager.CheckPasswordAsync(identity, password).GetAwaiter().GetResult();
            if (!isValidPassword)
                identity = null;
            return identity;
        }

        public async Task<SignUpResponse> Create(SignUpModel model)
        {

            var identity = new ApplicationUser();
            identity = UserManager.Users.Where(u => u.UserName == model.UserName).FirstOrDefault();
            if (model != null && identity == null)
            {
                var PortalMember = await _repository.CreatePortalMember(model);
                var user = new ApplicationUser() { 
                    UserName = model.UserName,
                    EmployeeID = 1,
                    FirstName = model.FirstName,
                    LastName = model.LastName,
                    RoleId = 2,
                    Email = model.Email, 
                    DateCreated = DateTime.Now, 
                    Activated = false,
                    PhoneNumber = model.PhoneNumber,
                    PortalMemberID = PortalMember.Id
                };

                var result = await UserManager.CreateAsync( user, model.Password);
                if(result.Succeeded)
                {
                    return new SignUpResponse { isSuccess = true, message = "Member Create Success" };
                }else
                {

                    return new SignUpResponse { isSuccess = false, message = "Member Create Failed" };
                }
            }else
            {

                return new SignUpResponse { isSuccess = false , message= $"{identity.UserName} this user already created, please try another"};
            }

        }
        public async Task<bool> CheckUserName(string username)
        {
            var identity = new ApplicationUser();
            identity = UserManager.Users.Where(u => u.UserName == username).FirstOrDefault();
            if (identity == null)
                return true;
            return false;
        }
    }
}
