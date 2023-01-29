using GC.MFI.DataAccess.InfrastructureBase;
using GC.MFI.DataAccess.Repository.Interfaces;
using GC.MFI.Models;
using GC.MFI.Models.DbModels;
using GC.MFI.Models.RequestModels;
using GC.MFI.Models.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GC.MFI.DataAccess.Repository.Implementations
{
    public class PortalSavingSummaryRepository : LegacyRepositoryBase<PortalSavingSummary>, IPortalSavingSummaryRepository
    {
        public PortalSavingSummaryRepository(IDatabaseFactory databaseFactory) : base(databaseFactory)
        {
        }

        public async Task<PortalSavingSummary> Create(PortalSavingSummary request)
        {
            BeginTransaction();
            var model = new PortalSavingSummary()
            {
                OfficeID = request.OfficeID,
                MemberID = request.MemberID,
                ProductID = request.ProductID,
                CenterID = request.CenterID,
                SavingInstallment = request.SavingInstallment,
                CreateDate = DateTime.UtcNow,
                CreateUser = request.CreateUser,
                OpeningDate = DateTime.UtcNow,
                MemberNomines = request.MemberNomines,
                ApprovalStatus = false
            };
            DataContext.Add(model);
            CommitTransaction();
            return model;
        }
        public async Task<PagedResponse<IEnumerable<SavingSummaryViewModel>>> GetAllPortalSavingSummaryPaged(PaginationFilter<SavingSummaryViewModel> filter, long Id)
        {
            var TotalElement = DataContext.PortalSavingSummary.Count(t => t.ApprovalStatus == true && t.MemberID == Id);

            var savingSummary =(from pps in DataContext.PortalSavingSummary
                                join pl in DataContext.Product on pps.ProductID equals pl.ProductID
                                join m in DataContext.Member on pps.MemberID equals m.MemberID
                                select new SavingSummaryViewModel
                                {
                                    PortalSavingSummaryID= pps.PortalSavingSummaryID,
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
                                    MaturedDate=pps.MaturedDate,
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
                                })
                                    .Where(filter.search)
                                    .Where(x => x.ApprovalStatus == true && x.MemberID == Id)
                                    .Skip(filter.pageNum > 0 ? (filter.pageNum - 1) * filter.pageSize : 0)
                                    .Take(filter.pageSize).ToList();
            //for(int i=0;i<savingSummary.Count();i++)
            //{
            //    var nominee = DataContext.NomineeXPortalSavingSummary.Where(t => t.PortalSavingSummaryID == savingSummary[i].PortalSavingSummaryID);
            //}

            return new PagedResponse<IEnumerable<SavingSummaryViewModel>>(
                savingSummary,
                filter.pageNum,
                filter.pageSize,
                TotalElement,
                TotalElement / filter.pageSize);
        }
    }
}