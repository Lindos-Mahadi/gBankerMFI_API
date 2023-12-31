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
    public interface ISavingSummaryService : ILegacyServiceBase<SavingSummary>
    {
        PagedResponse<IQueryable<SavingsSummaryViewModel>> GetAllPortalSavingSummaryPaged(PaginationFilter<SavingsSummaryViewModel> filter, long Id);
        SavingsSummaryViewModel SavingSummaryDetails(long Id);
    }
}
