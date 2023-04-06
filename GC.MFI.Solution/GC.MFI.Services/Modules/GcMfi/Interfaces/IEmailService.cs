using GC.MFI.Models.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GC.MFI.Services.Modules.GcMfi.Interfaces
{
    public interface IEmailService
    {
        Task<EmailResponse> SendOtp(string email);
        Task<EmailResponse> VerifyEmailOtpAsync(string email, string message);
    }
}
