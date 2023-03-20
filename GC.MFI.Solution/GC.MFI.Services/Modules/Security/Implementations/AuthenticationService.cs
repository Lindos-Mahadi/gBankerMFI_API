using AutoMapper;
using GC.MFI.DataAccess.InfrastructureBase;
using GC.MFI.DataAccess.Repository.Interfaces;
using GC.MFI.Models;
using GC.MFI.Models.DbModels;
using GC.MFI.Models.Modules.Distributions.Security;
using GC.MFI.Models.Modules.Security;
using GC.MFI.Models.ViewModels;
using GC.MFI.Services.Modules.GcMfi.Interfaces;
using GC.MFI.Services.Modules.Security.Interfaces;
using GC.MFI.Utility.Helpers;
using Microsoft.AspNetCore.Identity;
using System;
using System.Buffers.Text;
using System.Linq;
using System.Net.NetworkInformation;
using System.Reflection.Metadata;
using System.Threading.Tasks;

namespace GC.MFI.Services.Modules.Security.Implementations
{
    public class AuthenticationService : IAuthenticationService
    {
        private UserManager<ApplicationUser> _userManager;
        private IPortalMemberRepository _repository;
        private readonly IFileUploadService _fileService;
        private readonly IMapper mapper;
        private readonly IUnitOfWork unitOfWork;

        public AuthenticationService(UserManager<ApplicationUser> userManager, IUnitOfWork unitOfWork, IMapper mapper, IPortalMemberRepository repository, IFileUploadService fileService)
        {
            this._userManager = userManager;
            this._repository = repository;
            this._fileService = fileService;
            this.mapper = mapper;
            this.unitOfWork = unitOfWork;
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
            string[] imageTypes = { "image/jpg", "image/png", "image/jpeg" , "image/gif", "image/bmp", "image/tif", "image/tiff", "application/pdf" };
            if (model != null && identity == null)
            {
                var portalMember = mapper.Map<PortalMember>(model);
                portalMember.MemberCategoryID = 1;
                portalMember.MemberStatus = "AC";
                portalMember.Photo = "";
                portalMember.ApprovalStatus = false;
                portalMember.CreateDate = DateTime.Now;
                portalMember.UpdateDate = DateTime.Now;
                portalMember.Status = "A";
                int now = int.Parse(DateTime.Now.ToString("yyyyMMdd"));
                int dob = int.Parse(model.DOB.ToString("yyyyMMdd"));
                int age = (now - dob) / 10000;
                portalMember.MemberAge = age;
                _repository.Add(portalMember);
                unitOfWork.Commit();
                
                Base64File PNID = ImageHelper.GetFileDetails(model.NidPic);
                Base64File memImage = ImageHelper.GetFileDetails(model.Img);

                FileUploadTable[] file = new FileUploadTable[2]; 
                if (imageTypes.Contains(PNID.MimeType))
                {
                    file[0] = new FileUploadTable
                    {
                        EntityName = "PortalMember",
                        EntityId = portalMember.Id,
                        PropertyName = "MemberNID",
                        File = PNID.DataBytes,
                        FileName = $"{portalMember.FirstName} - {portalMember.Id}",
                        Type = PNID.MimeType,
                    };
                    
                }

                if (imageTypes.Contains(memImage.MimeType))
                {
                    file[1] = new FileUploadTable
                    {
                        EntityName = "PortalMember",
                        EntityId = portalMember.Id,
                        PropertyName = "Image",
                        File = memImage.DataBytes,
                        FileName = $"{portalMember.FirstName} - {portalMember.Id}",
                        Type = memImage.MimeType,
                    };
                }
                var InsertFiles = _fileService.BulkCreate(file);

                _repository.CreatePortalMemberNIDandImage(portalMember.Id, InsertFiles[0].FileUploadId, InsertFiles[1].FileUploadId);

                var user = new ApplicationUser() { 
                    UserName = model.UserName,
                    EmployeeID = 1,
                    FirstName = model.FirstName,
                    LastName = model.LastName,
                    RoleId = 14,
                    Email = model.Email,
                    DateCreated = DateTime.Now, 
                    Activated = false,
                    PhoneNumber = model.Phone,
                    PortalMemberID = portalMember.Id
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

        public async Task<ResponseStatus> ChangePassword(ChangePasswordModel CPM, string UserId)
        {
            ApplicationUser user = await _userManager.FindByIdAsync(UserId);
            var result = await _userManager.ChangePasswordAsync(user, CPM.OldPassword, CPM.NewPassword);
            if (result.Succeeded) 
            {
                return new ResponseStatus { IsSuccess = true , Message = "Password Change Successfully" };
            }else
            {
                return new ResponseStatus { IsSuccess = false , Message = "Given password wrong" };
            }
        }
    }
}
