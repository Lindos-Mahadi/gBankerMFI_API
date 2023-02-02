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
        private readonly GBankerDbContext _context;
        public PortalSavingSummaryRepository(IDatabaseFactory databaseFactory,GBankerDbContext context) : base(databaseFactory)
        {
            this._context= context;
        }

        //public async Task<PortalSavingSummary> Create(PortalSavingSummary request)
        //{
        //    BeginTransaction();
        //    var model = new PortalSavingSummary()
        //    {
        //        OfficeID = request.OfficeID,
        //        MemberID = request.MemberID,
        //        ProductID = request.ProductID,
        //        CenterID = request.CenterID,
        //        SavingInstallment = request.SavingInstallment,
        //        CreateDate = DateTime.UtcNow,
        //        CreateUser = request.CreateUser,
        //        OpeningDate = DateTime.UtcNow,
        //        MemberNomines = request.MemberNomines,
        //        ApprovalStatus = false
        //    };
        //    DataContext.Add(model);
        //    CommitTransaction();
        //    return model;
        //}

        public void CreatePortalSavingSummary(PortalSavingSummaryFileUpload entity)
        {
            BeginTransaction();
            var model = new PortalSavingSummary()
            {
                PortalSavingSummaryID= entity.PortalSavingSummaryID,
                OfficeID= entity.OfficeID,
                MemberID= entity.MemberID,
                ProductID= entity.ProductID,
                CenterID= entity.CenterID,
                NoOfAccount= entity.NoOfAccount,
                TransactionDate= entity.TransactionDate,
                Deposit= entity.Deposit,
                Withdrawal= entity.Withdrawal,
                Balance= entity.Balance,
                InterestRate= entity.InterestRate,
                SavingInstallment= entity.SavingInstallment,
                CumInterest = entity.CumInterest,
                MonthlyInterest = entity.MonthlyInterest,
                Penalty= entity.Penalty,
                OpeningDate= entity.OpeningDate,
                MaturedDate= entity.MaturedDate,
                ClosingDate= entity.ClosingDate,
                TransType= entity.TransType,
                SavingStatus= entity.SavingStatus,
                //EmployeeId = entity.EmployeeId,
                //MemberCategoryID= entity.MemberCategoryID,
                Posted = entity.Posted,
                IsActive= entity.IsActive,
                InActiveDate= entity.InActiveDate,
                Duration= entity.Duration,
                InstallmentNo= entity.InstallmentNo,
                CreateDate= entity.CreateDate,
                CreateUser= entity.CreateUser,
                //OrgID= entity.OrgID,
                SavingAccountNo= entity.SavingAccountNo,
                Ref_EmployeeID= entity.Ref_EmployeeID,
                ApprovalStatus= entity.ApprovalStatus,
                MemberNomines= entity.MemberNomines,
            };
            var portalSaveId = _context.PortalSavingSummary.Add(model);
            CommitTransaction();

            BeginTransaction();
            FileUploadTable[] file = new FileUploadTable[entity.PortalSavingFileUpload.Count];
            for (int i = 0; i < entity.PortalSavingFileUpload.Count(); i++)
            {
                file[i] = new FileUploadTable
                {

                    EntityId = model.PortalSavingSummaryID,
                    EntityName = "PortalSavingSummary",
                    PropertyName = entity.PortalSavingFileUpload[i].PropertyName,
                    FileName = entity.PortalSavingFileUpload[i].FileName,
                    Type = entity.PortalSavingFileUpload[i].Type,
                    File = System.Convert.FromBase64String(entity.PortalSavingFileUpload[i].File)
                };

            }
            _context.FileUploadTable.AddRange(file);
            CommitTransaction();
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
                                    MinLimit = pl.MinLimit,
                                    MaxLimit = pl.MaxLimit
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

        public async Task<IEnumerable<PortalSavingSummary>> getBySavingStatus(byte type, long memberId)
        {
            var getStatus = DataContext.PortalSavingSummary.Where(t => t.SavingStatus == type && t.MemberID == memberId);
            return getStatus;

        }
    }
}