using GC.MFI.DataAccess.InfrastructureBase;
using GC.MFI.DataAccess.Repository.Interfaces;
using GC.MFI.Models;
using GC.MFI.Models.DbModels;
using GC.MFI.Models.ViewModels;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GC.MFI.DataAccess.Repository.Implementations
{
    public class PortalLoanSummaryRepository : LegacyRepositoryBase<PortalLoanSummary>, IPortalLoanSummaryRepository
    {
        private readonly GBankerDbContext _context;
        public PortalLoanSummaryRepository(IDatabaseFactory databaseFactory, GBankerDbContext context) : base(databaseFactory)
        {
            this._context = context;
        }

        public void Create(PortalLoanSummary entity)
        {
            _context.PortalLoanSummary.Add(entity);
        }

        public IEnumerable<PortalLoanSummary> GetAllPortalLoanSummary()
        {
            return _context.PortalLoanSummary;
        }

        //public async Task<IEnumerable<Member>> GetAllMember(string search)
        //{
        //    if (!String.IsNullOrEmpty(search))
        //    {
        //        var memberList = DataContext.Member.Where(t => t.FirstName!.Contains(search) || t.FirstName.Trim()
        //        .Replace(" ", "").ToUpper()!.Contains(search.Trim().Replace(" ", "").ToUpper()));
        //        return memberList.Skip(0).Take(10);

        //    }
        //    return DataContext.Member.Skip(0).Take(10);
        //}
        public async Task<PagedResponse<IEnumerable<PortalLoanSummaryViewModel>>> GetAllPortalLoanSummaryPaged(PaginationFilter<PortalLoanSummaryViewModel> filter,long Id)
        {

            var totalElems = DataContext.PortalLoanSummary.Count(x => x.ApprovalStatus == true && x.MemberID == Id);
            var portalList = (from pls in DataContext.PortalLoanSummary
                              join prdct in DataContext.Product on pls.ProductID equals prdct.ProductID
                              join prpse in DataContext.Purpose on pls.PurposeID equals prpse.PurposeID
                              select new PortalLoanSummaryViewModel
                              {
                                  PortalLoanSummaryID = pls.PortalLoanSummaryID,
                                  OfficeID = pls.OfficeID,
                                  MemberID = pls.MemberID,
                                  ProductID = pls.ProductID,
                                  ProductName = prdct.ProductName,
                                  CenterID = pls.CenterID,
                                  MemberCategoryID = pls.MemberCategoryID,
                                  LoanTerm = pls.LoanTerm,
                                  PurposeID = pls.PurposeID,
                                  PurposeName = prpse.PurposeName,
                                  LoanNo = pls.LoanNo,
                                  PrincipalLoan = pls.PrincipalLoan,
                                  ApproveDate = pls.ApproveDate,
                                  DisburseDate = pls.DisburseDate,
                                  Duration = pls.Duration,
                                  LoanRepaid = pls.LoanRepaid,
                                  IntCharge = pls.IntCharge,
                                  IntPaid = pls.IntPaid,
                                  LoanInstallment = pls.LoanInstallment,
                                  IntInstallment = pls.IntInstallment,
                                  InterestRate = pls.InterestRate,
                                  InstallmentStartDate = pls.InstallmentStartDate,
                                  InstallmentNo = pls.InstallmentNo,
                                  DropInstallment = pls.DropInstallment,
                                  Holidays = pls.Holidays,
                                  InstallmentDate = pls.InstallmentDate,
                                  TransType = pls.TransType,
                                  ContinuousDrop = pls.ContinuousDrop,
                                  LoanStatus = pls.LoanStatus,
                                  Balance = pls.Balance,
                                  Advance = pls.Advance,
                                  DueRecovery = pls.DueRecovery,
                                  LoanCloseDate = pls.LoanCloseDate,
                                  OverdueDate = pls.OverdueDate,
                                  EmployeeId = pls.EmployeeId,
                                  InvestorID = pls.InvestorID,
                                  ExcessPay = pls.ExcessPay,
                                  CurLoan = pls.CurLoan,
                                  PreLoan = pls.PreLoan,
                                  CumLoanDue = pls.CumLoanDue,
                                  WriteOffLoan = pls.WriteOffLoan,
                                  WriteOffInterest = pls.WriteOffInterest,
                                  Posted = pls.Posted,
                                  OrgID = pls.OrgID,
                                  IsActive = pls.IsActive,
                                  InActiveDate = pls.InActiveDate,
                                  CreateUser = pls.CreateUser,
                                  CreateDate = pls.CreateDate,
                                  BankName = pls.BankName,
                                  ChequeNo = pls.ChequeNo,
                                  IsApproved = pls.IsApproved,
                                  CoApplicantName = pls.CoApplicantName,
                                  Guarantor = pls.Guarantor,
                                  MemberPassBookRegisterID = pls.MemberPassBookRegisterID,
                                  ChequeIssueDate = pls.ChequeIssueDate,
                                  CumIntDue = pls.CumIntDue,
                                  ApprovedAmount = pls.ApprovedAmount,
                                  PartialAmount = pls.PartialAmount,
                                  FinalDisbursement = pls.FinalDisbursement,
                                  DisbursementType = pls.DisbursementType,
                                  PartialIntCharge = pls.PartialIntCharge,
                                  PartialIntPaid = pls.PartialIntPaid,
                                  FirstInstallmentStartDate = pls.FirstInstallmentStartDate,
                                  FirstInstallmentDate = pls.FirstInstallmentDate,
                                  CurIntPaid = pls.CurIntPaid,
                                  CurIntCharge = pls.CurIntCharge,
                                  LoanAccountNo = pls.LoanAccountNo,
                                  SecurityBankName = pls.SecurityBankName,
                                  SecurityBankBranchName = pls.SecurityBankBranchName,
                                  SecurityBankCheckNo = pls.SecurityBankCheckNo,
                                  CurLoanDue = pls.CurLoanDue,
                                  CurIntDue = pls.CurIntDue,
                                  LastInstallmentNo = pls.LastInstallmentNo,
                                  CSFRate = pls.CSFRate,
                                  CSFAmount = pls.CSFAmount,
                                  Remarks = pls.Remarks,
                                  ApprovalStatus = pls.ApprovalStatus
                              }
                              ).Where(filter.search)
                               .Where(x => x.ApprovalStatus == true && x.MemberID == Id)
                               .Skip(filter.pageNum > 0 ? (filter.pageNum - 1) * filter.pageSize : 0)
                               .Take(filter.pageSize);

            return new PagedResponse<IEnumerable<PortalLoanSummaryViewModel>>(
                portalList,
                filter.pageNum,
                filter.pageSize,
                totalElems,
                totalElems / filter.pageSize);
            
        }
    }
}
