using GC.MFI.Models;
using GC.MFI.Models.DbModels;
using GC.MFI.Models.RequestModels;
using GC.MFI.Models.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GC.MFI.Services.Modules.GcMfi.Interfaces
{
    public interface IPortalSavingSummaryService : ILegacyServiceBase<PortalSavingSummary>
    {
        //Task<PortalSavingSummary> Create(PortalSavingSummary request);
        Task<PagedResponse<IEnumerable<SavingSummaryViewModel>>> GetAllPortalSavingSummaryPaged(PaginationFilter<SavingSummaryViewModel> filter , long Id);

        Task<IEnumerable<SavingSummaryViewModel>> getBySavingStatus(byte type, long memberId);
        Task<IEnumerable<SavingSummaryViewModel>> getBySavingStatus(long memberId);

        PortalSavingSummary CreatePortalSavingSummary(PortalSavingSummaryFileUpload entity);
        Task<PortalSavingSummaryViewModel> PortalSavingSummaryDetails(long Id);
    }
}
