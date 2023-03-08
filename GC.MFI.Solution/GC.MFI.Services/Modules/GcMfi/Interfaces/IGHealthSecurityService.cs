using GC.MFI.Models.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GC.MFI.Services.Modules.GcMfi.Interfaces
{
    public interface IGHealthSecurityService
    {
        Task<List<GHealthPatientViewModel>> IsGHealthLoggedIn(string mobile);

        Task<GHealthSignViewModel> Authenticate(string username, string password);
    }
}
