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
    public interface ILoanSummaryService : ILegacyServiceBase<LoanSummary>
    {
        PagedResponse<IQueryable<LoanSummaryViewModel>> GetAllPortalLoanSummaryPaged(PaginationFilter<LoanSummaryViewModel> filter, long Id);
        LoanSummaryViewModel GetById(long id);
    }
}
