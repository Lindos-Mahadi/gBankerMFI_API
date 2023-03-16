using GC.MFI.DataAccess.InfrastructureBase;
using GC.MFI.DataAccess.Repository.Interfaces;
using GC.MFI.Models;
using GC.MFI.Models.DbModels;
using GC.MFI.Models.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GC.MFI.DataAccess.Repository.Implementations
{
    public class LoanSummaryRepository : LegacyRepositoryBase<LoanSummary>, ILoanSummaryRepository
    {
        public LoanSummaryRepository(IDatabaseFactory databaseFactory) : base(databaseFactory)
        {
        }

        public PagedResponse<IQueryable<LoanSummaryViewModel>> GetAllPortalLoanSummaryPaged(PaginationFilter<LoanSummaryViewModel> filter, long Id)
        {
            var totalElems = DataContext.LoanSummary.Count(x => x.MemberID == Id);
            var portalList = (from pls in DataContext.LoanSummary
                              join prdct in DataContext.Product on pls.ProductID equals prdct.ProductID
                              join prpse in DataContext.Purpose on pls.PurposeID equals prpse.PurposeID
                              select new LoanSummaryViewModel
                              {
                                  LoanSummaryID = pls.LoanSummaryID,
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
                                  ApprovedAmount = pls.ApprovedAmount,
                                  PartialAmount = pls.PartialAmount,
                                  FinalDisbursement = pls.FinalDisbursement,
                                  DisbursementType = pls.DisbursementType,
                                  PartialIntCharge = pls.PartialIntCharge,
                                  PartialIntPaid = pls.PartialIntPaid,
                                  FirstInstallmentStartDate = pls.FirstInstallmentStartDate,
                                  FirstInstallmentDate = pls.FirstInstallmentDate,
                                  LoanAccountNo = pls.LoanAccountNo,
                                  SecurityBankName = pls.SecurityBankName,
                                  SecurityBankBranchName = pls.SecurityBankBranchName,
                                  SecurityBankCheckNo = pls.SecurityBankCheckNo,
                                  Remarks = pls.Remarks,
                              }
                              ).Where(x => x.MemberID == Id)
                               .Where(filter.search)
                               .OrderByDescending(t => t.LoanSummaryID)
                               .Skip(filter.pageNum > 0 ? (filter.pageNum - 1) * filter.pageSize : 0)
                               .Take(filter.pageSize > 0 ? filter.pageSize : totalElems);
            var totalPages = ((double)totalElems / (double)filter.pageSize);
            if (filter.pageSize < 0)
            {
                totalPages = ((double)totalElems / (double)totalElems);
            }
            int roundedTotalPages = Convert.ToInt32(Math.Ceiling(totalPages));
            return new PagedResponse<IQueryable<LoanSummaryViewModel>>(
                portalList,
                filter.pageNum,
                filter.pageSize,
                totalElems,
                roundedTotalPages);
        }
    }
}
