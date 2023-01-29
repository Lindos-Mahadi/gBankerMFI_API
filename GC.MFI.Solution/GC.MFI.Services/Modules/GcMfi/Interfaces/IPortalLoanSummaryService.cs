using GC.MFI.Models;
using GC.MFI.Models.DbModels;
using GC.MFI.Models.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GC.MFI.Services.Modules.GcMfi.Interfaces
{
    public interface IPortalLoanSummaryService : ILegacyServiceBase<PortalLoanSummary>
    {
        void Create(PortalLoanSummary entity);
        IEnumerable<PortalLoanSummary> GetAllPortalLoanSummary();
        //IEnumerable<PortalLoanSummary> GetAll(Expression<Func<PortalLoanSummary, bool>> where);

        Task<PagedResponse<IEnumerable<PortalLoanSummaryViewModel>>> GetAllPortalLoanSummaryPaged(PaginationFilter<PortalLoanSummaryViewModel> filter, long Id);
    }
}
