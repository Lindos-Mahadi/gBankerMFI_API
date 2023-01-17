using GC.MFI.Models.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GC.MFI.Services.Modules.GcMfi.Interfaces
{
    public interface INIDService
    {
        Task<NIDVerificationResponse> GetNIDInfo(NIDVerificationRequest request);
    }
}
