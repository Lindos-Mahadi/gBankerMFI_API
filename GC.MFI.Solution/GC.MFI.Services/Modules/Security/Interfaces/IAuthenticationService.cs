﻿using GC.MFI.Models.DbModels;
using GC.MFI.Models.Modules.Distributions.Security;
using GC.MFI.Models.Modules.Security;
using GC.MFI.Models.ViewModels;
using Microsoft.AspNetCore.Identity;
using System.Threading.Tasks;

namespace GC.MFI.Services.Modules.Security.Interfaces
{
    public interface IAuthenticationService
    {
        Task<ApplicationUser> Authenticate(string username, string password);
        Task<SignUpResponse> Create(SignUpModel model);
        Task<bool> CheckUserName(string username);
        Task<ResponseStatus> ChangePassword(ChangePasswordModel CPM, string UserId);
    }
}
