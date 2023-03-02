using GC.MFI.Models.DbModels;
using GC.MFI.Models.Models;
using GC.MFI.Models.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GC.MFI.Services.Modules.GcMfi.Interfaces
{
    public interface ISMSTwilioService
    {
        Task<TwilioSMSModel> SendOtpAsync(string mobileNo);
        Task<TwilioSMSModel> VerifyOtpAsync(string mobileNo, string message);
    }
}
