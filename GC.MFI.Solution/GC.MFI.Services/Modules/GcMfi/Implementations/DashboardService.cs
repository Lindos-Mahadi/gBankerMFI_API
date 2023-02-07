using GC.MFI.DataAccess.Repository.Interfaces;
using GC.MFI.Models.ViewModels;
using GC.MFI.Services.Modules.GcMfi.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GC.MFI.Services.Modules.GcMfi.Implementations
{
    public class DashboardService : IDashboardService
    {
        private readonly IPortalLoanSummaryRepository portalLoanSummaryRepository;
        private readonly IPortalSavingSummaryRepository portalSavingSummaryRepository;
        public DashboardService(IPortalLoanSummaryRepository portalLoanSummaryRepository , IPortalSavingSummaryRepository portalSavingSummaryRepository)
        {
            this.portalLoanSummaryRepository = portalLoanSummaryRepository;
            this.portalSavingSummaryRepository = portalSavingSummaryRepository ;
        }

        public async Task<DashboardModel> GetDashboardInfo(long MemberId)
        {
            var loanSummary = portalLoanSummaryRepository.GetMany(t => t.MemberID == MemberId && t.LoanStatus == 3);
            var loanSummarySum = loanSummary.Sum(t => t.PrincipalLoan);

            var savingSummary = portalSavingSummaryRepository.GetMany(t => t.MemberID == MemberId && t.SavingStatus == 3);
                
             var savingSummarySum = savingSummary.Sum(t => t.Balance);
            var totalProduct = loanSummary.Count() + savingSummary.Count();

            return new DashboardModel { AmmountOfLoan = (decimal)loanSummarySum, AmmountOfSaving = (decimal)savingSummarySum , AmmountOfProduct = totalProduct };
        }
    }
}
