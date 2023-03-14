using GC.MFI.DataAccess.InfrastructureBase;
using GC.MFI.DataAccess.Repository.Interfaces;
using GC.MFI.Models;
using GC.MFI.Models.DbModels;
using GC.MFI.Models.ViewModels;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GC.MFI.DataAccess.Repository.Implementations
{
    public class SavingSummaryRepository : LegacyRepositoryBase<SavingSummary>, ISavingSummaryRepository
    {
        public SavingSummaryRepository(IDatabaseFactory databaseFactory) : base(databaseFactory)
        {
        }

        public PagedResponse<IQueryable<SavingsSummaryViewModel>> GetAllPortalSavingSummaryPaged(PaginationFilter<SavingsSummaryViewModel> filter, long Id)
        {
            var TotalElement = DataContext.SavingSummary.Count(t => t.MemberID == Id);

            var savingSummary = (from pps in DataContext.SavingSummary
                                 join pl in DataContext.Product on pps.ProductID equals pl.ProductID
                                 join m in DataContext.Member on pps.MemberID equals m.MemberID
                                 select new SavingsSummaryViewModel
                                 {
                                     SavingSummaryID = pps.SavingSummaryID,
                                     OfficeID = pps.OfficeID,
                                     MemberID = pps.MemberID,
                                     MemberName = m.FirstName,
                                     ProductID = (short)pl.ProductID,
                                     ProductName = pl.ProductName,
                                     CenterID = pps.CenterID,
                                     NoOfAccount = pps.NoOfAccount,
                                     TransactionDate = pps.TransactionDate,
                                     Deposit = pps.Deposit,
                                     Withdrawal = pps.Withdrawal,
                                     Balance = pps.Balance,
                                     InterestRate = pps.InterestRate,
                                     SavingInstallment = pps.SavingInstallment,
                                     CumInterest = pps.CumInterest,
                                     MonthlyInterest = pps.MonthlyInterest,
                                     Penalty = pps.Penalty,
                                     OpeningDate = pps.OpeningDate,
                                     MaturedDate = pps.MaturedDate,
                                     ClosingDate = pps.ClosingDate,
                                     TransType = pps.TransType,
                                     SavingStatus = pps.SavingStatus,
                                     EmployeeId = pps.EmployeeId,
                                     MemberCategoryID = pps.MemberCategoryID,
                                     Posted = pps.Posted,
                                     IsActive = pps.IsActive,
                                     InActiveDate = pps.InActiveDate,
                                     CreateDate = pps.CreateDate,
                                     CreateUser = pps.CreateUser,
                                     OrgID = pps.OrgID,
                                     SavingAccountNo = pps.SavingAccountNo,
                                     MinLimit = pl.MinLimit,
                                     MaxLimit = pl.MaxLimit
                                 })
                                    .Where(x => x.MemberID == Id)
                                    .Where(filter.search)
                                    .OrderByDescending(t => t.SavingSummaryID)
                                    .Skip(filter.pageNum > 0 ? (filter.pageNum - 1) * filter.pageSize : 0)
                                    .Take(filter.pageSize);

            var totalPages = Convert.ToInt32(Math.Ceiling(((double)TotalElement / (double)filter.pageSize)));
            return new PagedResponse<IQueryable<SavingsSummaryViewModel>>(
                savingSummary,
                filter.pageNum,
                filter.pageSize,
                TotalElement,
                totalPages);
        }
    }
}
