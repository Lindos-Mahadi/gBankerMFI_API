using GC.MFI.DataAccess.InfrastructureBase;
using GC.MFI.Models;
using GC.MFI.Models.DbModels;
using GC.MFI.Models.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace GC.MFI.DataAccess.Repository.Interfaces
{
    public interface IPortalLoanSummaryRepository : ILegacyRepository<PortalLoanSummary>
    {
        void CreatePortalLoanSummary(PortalLoanSummaryFileUpload entity);
        IEnumerable<PortalLoanSummary> GetAllPortalLoanSummary();
        Task<PagedResponse<IEnumerable<PortalLoanSummaryViewModel>>> GetAllPortalLoanSummaryPaged(PaginationFilter<PortalLoanSummaryViewModel> filter, long Id);
        //IEnumerable<PortalLoanSummary> GetAll(Expression<Func<PortalLoanSummary, bool>> where);
        Task<IEnumerable<PortalLoanSummaryViewModel>> getByLoanStatus(byte type, long memberId);
    }
}
