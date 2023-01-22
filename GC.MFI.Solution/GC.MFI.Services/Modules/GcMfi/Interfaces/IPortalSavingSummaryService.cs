using GC.MFI.Models.DbModels;
using GC.MFI.Models.RequestModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GC.MFI.Services.Modules.GcMfi.Interfaces
{
    public interface IPortalSavingSummaryService : ILegacyServiceBase<PortalSavingSummary>
    {
        Task<PortalSavingSummary> Create(SavingAccountModel request);
    }
}
