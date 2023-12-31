﻿using GC.MFI.Models;
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
        PortalLoanSummary CreatePortalLoanSummary(PortalLoanSummaryFileUpload entity);
        IEnumerable<PortalLoanSummary> GetAllPortalLoanSummary();
        Task<PagedResponse<IEnumerable<PortalLoanSummaryViewModel>>> GetAllPortalLoanSummaryPaged(PaginationFilter<PortalLoanSummaryViewModel> filter, long Id);
        Task<IEnumerable<PortalLoanSummaryViewModel>> getByLoanStatus(byte type, long memberId);
        Task<IEnumerable<PortalLoanSummaryViewModel>> getByLoanStatus(long memberId);
        PortalLoanSummaryViewModel GetById(long id);
    }
}
