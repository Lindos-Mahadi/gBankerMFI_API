using GC.MFI.DataAccess.InfrastructureBase;
using GC.MFI.DataAccess.Repository.Interfaces;
using GC.MFI.Models;
using GC.MFI.Models.DbModels;
using GC.MFI.Models.RequestModels;
using GC.MFI.Models.ViewModels;
using GC.MFI.Utility.Helpers;
using Microsoft.EntityFrameworkCore;
using System;
using System.Buffers.Text;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GC.MFI.DataAccess.Repository.Implementations
{
    public class PortalSavingSummaryRepository : LegacyRepositoryBase<PortalSavingSummary>, IPortalSavingSummaryRepository
    {
        public PortalSavingSummaryRepository(IDatabaseFactory databaseFactory,GBankerDbContext context) : base(databaseFactory)
        {
        }

        

        public async Task<PagedResponse<IEnumerable<SavingSummaryViewModel>>> GetAllPortalSavingSummaryPaged(PaginationFilter<SavingSummaryViewModel> filter, long Id)
        {
            var TotalElement = DataContext.PortalSavingSummary.Count(t => t.MemberID == Id);

            var savingSummary =(from pps in DataContext.PortalSavingSummary
                                join pl in DataContext.Product on pps.ProductID equals pl.ProductID
                                join m in DataContext.Member on pps.MemberID equals m.MemberID
                                select new SavingSummaryViewModel
                                {
                                    PortalSavingSummaryID= pps.PortalSavingSummaryID,
                                    SavingSummaryID = pps.SavingSummaryID,
                                    OfficeID=pps.OfficeID,
                                    MemberID=pps.MemberID,
                                    MemberName = m.FirstName,
                                    ProductID= (short)pl.ProductID,
                                    ProductName= pl.ProductName,
                                    CenterID=pps.CenterID,
                                    NoOfAccount=pps.NoOfAccount,
                                    TransactionDate = pps.TransactionDate,
                                    Deposit = pps.Deposit,
                                    Withdrawal = pps.Withdrawal,
                                    Balance= pps.Balance,
                                    InterestRate=pps.InterestRate,
                                    SavingInstallment=pps.SavingInstallment,
                                    CumInterest = pps.CumInterest,
                                    MonthlyInterest = pps.MonthlyInterest,
                                    Penalty=pps.Penalty,
                                    OpeningDate=pps.OpeningDate,
                                    MaturedDate= pps.MaturedDate,
                                    ClosingDate=pps.ClosingDate,
                                    TransType=pps.TransType,
                                    SavingStatus=pps.SavingStatus,
                                    EmployeeId = pps.EmployeeId,
                                    MemberCategoryID=pps.MemberCategoryID,
                                    Posted = pps.Posted,
                                    IsActive=pps.IsActive,
                                    InActiveDate=pps.InActiveDate,
                                    CreateDate=pps.CreateDate,
                                    CreateUser=pps.CreateUser,
                                    OrgID=pps.OrgID,
                                    SavingAccountNo = pps.SavingAccountNo,
                                    Ref_EmployeeID=pps.Ref_EmployeeID,
                                    ApprovalStatus = pps.ApprovalStatus,
                                    MemberNomines =pps.MemberNomines,
                                    MinLimit = pl.MinLimit,
                                    MaxLimit = pl.MaxLimit
                                })
                                    .Where(filter.search)
                                    .Where(x => x.MemberID == Id)
                                    .OrderByDescending(t => t.PortalSavingSummaryID)
                                    .Skip(filter.pageNum > 0 ? (filter.pageNum - 1) * filter.pageSize : 0)
                                    .Take(filter.pageSize).ToList();
            
            var totalPages = Convert.ToInt32(Math.Ceiling(((double)TotalElement / (double)filter.pageSize)));
            return new PagedResponse<IEnumerable<SavingSummaryViewModel>>(
                savingSummary,
                filter.pageNum,
                filter.pageSize,
                TotalElement,
                totalPages);
        }

        public async Task<IEnumerable<SavingSummaryViewModel>> getBySavingStatus(byte type, long memberId)
        {
            var savingSummary = (from pps in DataContext.PortalSavingSummary
                                 join pl in DataContext.Product on pps.ProductID equals pl.ProductID
                                 join m in DataContext.Member on pps.MemberID equals m.MemberID
                                 select new SavingSummaryViewModel
                                 {
                                     PortalSavingSummaryID = pps.PortalSavingSummaryID,
                                     OfficeID = pps.OfficeID,
                                     MemberID = pps.MemberID,
                                     MemberName = m.FirstName,
                                     ProductID = (short)pl.ProductID,
                                     ProductName = pl.ProductName,
                                     CenterID = pps.CenterID,
                                     Balance = pps.Balance,
                                     SavingInstallment = pps.SavingInstallment,
                                     SavingStatus = pps.SavingStatus,
                                     IsActive = pps.IsActive,
                                     InActiveDate = pps.InActiveDate,
                                     CreateDate = pps.CreateDate,
                                     CreateUser = pps.CreateUser,
                                     OrgID = pps.OrgID,
                                     ApprovalStatus = pps.ApprovalStatus,
                                     MinLimit = pl.MinLimit,
                                     MaxLimit = pl.MaxLimit
                                 }).Where(t => t.SavingStatus == type && t.MemberID == memberId);
                           
            return savingSummary;
        }
        public override PortalSavingSummary GetById(long id)
        {
            var GetByid= DataContext.PortalSavingSummary.Where(t=> t.PortalSavingSummaryID == id).Include(t=> t.MemberNomines).FirstOrDefault();
            return GetByid;
        }

    }
}