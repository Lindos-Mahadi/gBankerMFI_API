using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
namespace GC.MFI.Models
{
    public class SecurityModel
    {
        public string TenantId { get; set; }
        public string TenantLoginId { get; set; }
        public string Token { get; set; }
        public string BookingRecordId { get; set; }

    }
    public class LoginModel
    {
        public string UserId { get; set; }
        public string Password { get; set; }
        public string UserName { get; set; }
    }
    public class ForgotPasswordModel
    {
        public string emailId { get; set; }    
        public string url { get; set; }
    }
    public class UpdatePasswordModel
    {
        public string UserId { get; set; }
        public string userToken { get; set; }
        public string Password { get; set; }
    }
    public class UserModel
    {
        public string UserId { get; set; }
        public string oldPassword { get; set; }
        public string newPassword { get; set; }
    }
    public class SignUpModel
    {
        public int Id { get; set; }
        public string FullName { get; set; }
        public string UserName { get; set; }
        public string Password { get; set; }
        public bool IsComplete { get; set; }
        public string Email { get; set; }
        // public bool IsActive { get; set; } = true;
    }
    
    public class AuthenticationResponseModel
    {
        public bool isSuccesful { get; set; }
        public string message { get; set; }
        public string token { get; set; }
        public string loginName { get; set; }
        public string fullName { get; set; }
        public string role { get; set; }
        public string designation { get; set; }
        public string email { get; set; }

        public string userId { get; set; }
    }

    public class AuthenticationProductResponseModel
    {
        public bool isSuccesful { get; set; }
        public string message { get; set; }
        public string token { get; set; }
        public string Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public int Price { get; set; }

    }
}
