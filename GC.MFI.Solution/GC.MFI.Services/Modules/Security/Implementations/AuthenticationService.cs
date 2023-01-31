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
        private UserManager<ApplicationUser> _userManager;
        private IPortalMemberRepository _repository;
        private readonly IFileUploadRepository _repositoryFile;

        public AuthenticationService(UserManager<ApplicationUser> userManager, IPortalMemberRepository repository, IFileUploadRepository repositoryFile)
        {
            this._userManager = userManager;
            this._repository = repository;
            this._repositoryFile = repositoryFile;
        }
        public async Task<ApplicationUser> Authenticate(string username, string password)
        {
            var identity = new ApplicationUser();
            identity = await _userManager.FindByNameAsync(username);
          
            var isValidPassword = false;
            if (identity == null)
                identity = null;
            if (identity != null)
                isValidPassword = _userManager.CheckPasswordAsync(identity, password).GetAwaiter().GetResult();
            if (!isValidPassword)
                identity = null;
            return identity;
        }

        public async Task<SignUpResponse> Create(SignUpModel model)
        {

            var identity = new ApplicationUser();
            identity = _userManager.Users.Where(u => u.UserName == model.UserName).FirstOrDefault();
            if (model != null && identity == null)
            {
                var PortalMember = await _repository.CreatePortalMember(model);
                //string[] image = model.NidPic.Split(new Char[] { ':', ';', ',' });
                //string imageType = image[1];
                //string imageUrl = image[3];
              //  if (imageUrl != null)
              //  {
                    var fileCreate = new FileUploadTable
                    {
                        EntityName = "Member",
                        EntityId = PortalMember.Id,
                        PropertyName = "MemberNID",
                        File = model.NidPic,
                        FileName = $"{PortalMember.FirstName} - {PortalMember.Id}",
                        Type = "jpeg",
                    };
                    await _repositoryFile.CreateFileUpload(fileCreate);
              //  }
               
                
                var user = new ApplicationUser() { 
                    UserName = model.UserName,
                    EmployeeID = 1,
                    FirstName = model.FirstName,
                    LastName = model.LastName,
                    RoleId = 14,
                    Email = model.Email, 
                    DateCreated = DateTime.Now, 
                    Activated = false,
                    PhoneNumber = model.PhoneNumber,
                    PortalMemberID = PortalMember.Id
                };

                var result = await _userManager.CreateAsync( user, model.Password);
                if(result.Succeeded)
                {
                    await _userManager.AddToRoleAsync(user, "PortalMember");
                    return new SignUpResponse { isSuccess = true, message = "Member Create Success" };
                }else
                {

                    return new SignUpResponse { isSuccess = false, message = "Member Create Failed" };
                }
            }else
            {

                return new SignUpResponse { isSuccess = false , message= $"{identity.UserName} already exist"};
            }

        }
        public async Task<bool> CheckUserName(string username)
        {
            var identity = new ApplicationUser();
            identity = _userManager.Users.Where(u => u.UserName == username).FirstOrDefault();
            if (identity == null)
                return true;
            return false;
        }
    }
}
