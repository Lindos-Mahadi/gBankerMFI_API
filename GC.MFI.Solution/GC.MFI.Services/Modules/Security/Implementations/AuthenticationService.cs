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

        public AuthenticationService(UserManager<ApplicationUser> _userManager)
        {
            this.UserManager = _userManager;
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
            if (model != null)
            {
                var j = new ApplicationUser() { 
                    UserName = model.UserName,
                    EmployeeID = 1,
                    FirstName = "ABCD",
                    RoleId = 2,
                    Email = model.Email, 
                    DateCreated = DateTime.Now, 
                    Activated = false,
                };
                //var user = new ApplicationUser
                //{
                //    UserName = model.UserName,
                //    Email = model.Email,
                //    PhoneNumber = model.PhoneNumber,
                //    FirstName = model.FirstName,
                //    LastName = model.LastName,
                //    Occupation = model.Occupation,
                //    Address = model.Address,
                //    NidPic = model.NidPic,
                //    MemberCode = model.MemberCode,
                //    OfficeID = model.OfficeID,
                //    GroupID = model.GroupID,
                //    CenterID = model.CenterID,
                //    JoinDate = model.JoinDate,
                //    Gender = model.Gender,
                //    MemberStatus = "A",
                //    MemberCategoryID = model.MemberCategoryID,
                //    OrgID = model.OrgID,
                //    CreateUser = model.CreateUser,
                //    CreateDate = DateTime.Now
                //};
                var result = await UserManager.CreateAsync( j, model.Password);
                if(result.Succeeded)
                {
                    return new SignUpResponse { isSuccess = true, message = "Member Create Success" };
                }else
                {

                    return new SignUpResponse { isSuccess = false, message = "Member Create Failed" };
                }
            }else
            {

                throw new Exception("Please enter Valid Model.");
            }

        }
    }
}
