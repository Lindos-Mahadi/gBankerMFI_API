using GC.MFI.DataAccess.InfrastructureBase;
using GC.MFI.DataAccess.Repository.Interfaces;
using GC.MFI.Models;
using GC.MFI.Models.DbModels;
using GC.MFI.Models.ViewModels;
using GC.MFI.Utility.Helpers;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;

namespace GC.MFI.DataAccess.Repository.Implementations
{
    public class PortalLoanSummaryRepository : LegacyRepositoryBase<PortalLoanSummary>, IPortalLoanSummaryRepository
    {
        private readonly GBankerDbContext _context;
        public PortalLoanSummaryRepository(IDatabaseFactory databaseFactory, GBankerDbContext context) : base(databaseFactory)
        {
            this._context = context;
        }

        public void CreatePortalLoanSummary(PortalLoanSummaryFileUpload entity)
        {
            BeginTransaction();
            
            var portal = new PortalLoanSummary
            {
                PortalLoanSummaryID= entity.PortalLoanSummaryID,
                OfficeID = entity.OfficeID,
                MemberID = entity.MemberID,
                ProductID = entity.ProductID,
                CenterID = entity.CenterID,
                MemberCategoryID = entity.MemberCategoryID,
                LoanTerm = entity.LoanTerm,
                PurposeID = entity.PurposeID,
                LoanNo = entity.LoanNo,
                PrincipalLoan = entity.PrincipalLoan,
                ApproveDate = entity.ApproveDate,
                DisburseDate = entity.DisburseDate,
                Duration = entity.Duration,
                LoanRepaid = entity.LoanRepaid,
                IntCharge = entity.IntCharge,
                IntPaid = entity.IntPaid,
                LoanInstallment = entity.LoanInstallment,
                IntInstallment = entity.IntInstallment,
                InterestRate = entity.InterestRate,
                InstallmentStartDate = entity.InstallmentStartDate,
                InstallmentNo = entity.InstallmentNo,
                DropInstallment = entity.DropInstallment,
                Holidays = entity.Holidays,
                InstallmentDate = entity.InstallmentDate,
                TransType = entity.TransType,
                ContinuousDrop = entity.ContinuousDrop,
                LoanStatus = entity.LoanStatus,
                Balance = entity.Balance,
                Advance = entity.Advance,
                DueRecovery = entity.DueRecovery,
                LoanCloseDate = entity.LoanCloseDate,
                OverdueDate = entity.OverdueDate,
                EmployeeId = entity.EmployeeId,
                InvestorID = entity.InvestorID,
                ExcessPay = entity.ExcessPay,
                CurLoan = entity.CurLoan,
                PreLoan = entity.PreLoan,
                CumLoanDue = entity.CumLoanDue,
                WriteOffLoan = entity.WriteOffLoan,
                WriteOffInterest = entity.WriteOffInterest,
                Posted = entity.Posted,
                OrgID = entity.OrgID,
                IsActive = entity.IsActive,
                InActiveDate = entity.InActiveDate,
                CreateUser = entity.CreateUser,
                CreateDate = entity.CreateDate,
                BankName = entity.BankName,
                ChequeNo = entity.ChequeNo,
                IsApproved = entity.IsApproved,
                CoApplicantName = entity.CoApplicantName,
                Guarantor = entity.Guarantor,
                MemberPassBookRegisterID = entity.MemberPassBookRegisterID,
                ChequeIssueDate = entity.ChequeIssueDate,
                CumIntDue = entity.CumIntDue,
                ApprovedAmount = entity.ApprovedAmount,
                PartialAmount = entity.PartialAmount,
                FinalDisbursement = entity.FinalDisbursement,
                DisbursementType = entity.DisbursementType,
                PartialIntCharge = entity.PartialIntCharge,
                PartialIntPaid = entity.PartialIntPaid,
                FirstInstallmentStartDate = entity.FirstInstallmentStartDate,
                FirstInstallmentDate = entity.FirstInstallmentDate,
                CurIntPaid = entity.CurIntPaid,
                CurIntCharge = entity.CurIntCharge,
                LoanAccountNo = entity.LoanAccountNo,
                SecurityBankName = entity.SecurityBankName,
                SecurityBankBranchName = entity.SecurityBankBranchName,
                SecurityBankCheckNo = entity.SecurityBankCheckNo,
                CurLoanDue = entity.CurLoanDue,
                CurIntDue = entity.CurIntDue,
                LastInstallmentNo = entity.LastInstallmentNo,
                CSFRate = entity.CSFRate,
                CSFAmount = entity.CSFAmount,
                Remarks = entity.Remarks,
                ApprovalStatus = entity.ApprovalStatus
            };
             _context.PortalLoanSummary.Add(portal);
            CommitTransaction();
            BeginTransaction();

            // For GuarantorNID & image
            Base64File nidType = ImageHelper.GetFileDetails(entity.GuarantorNID);
            var GuarantorNID = new FileUploadTable
            {
                EntityId = portal.PortalLoanSummaryID,
                EntityName = "PortalLoanSummary",
                PropertyName = "GuarantorNID",
                FileName = $"GuarantorNID_{portal.PortalLoanSummaryID}",
                File = nidType.DataBytes,
                Type = nidType.MimeType
            };

            Base64File gPhotoType = ImageHelper.GetFileDetails(entity.GuarantorImg);
            var GuarantorPhoto = new FileUploadTable
            {
                EntityId = portal.PortalLoanSummaryID,
                EntityName = "PortalLoanSummary",
                PropertyName = "GuarantorImage",
                FileName = $"GuarantorImage_{portal.PortalLoanSummaryID}",
                File = gPhotoType.DataBytes,
                Type = gPhotoType.MimeType
            };
            DataContext.FileUploadTable.Add(GuarantorNID);
            DataContext.FileUploadTable.Add(GuarantorPhoto);
            // For bulk insert
            FileUploadTable[] file = new FileUploadTable[entity.PortalLoanFileUpload.Count];
            for (int i = 0; i < entity.PortalLoanFileUpload.Count(); i++)
            {
                Base64File filesTypes = ImageHelper.GetFileDetails(entity.PortalLoanFileUpload[i].File);
                file[i] = new FileUploadTable
                {

                    EntityId = portal.PortalLoanSummaryID,
                    EntityName = "PortalLoanSummary",
                    PropertyName = "SupportingDocument",
                    FileName = $"SupportingDocument_L{portal.PortalLoanSummaryID}_{i + 1}",
                    Type = filesTypes.MimeType,
                    File = filesTypes.DataBytes,
                    DocumentType = entity.PortalLoanFileUpload[i].DocumentType
                };

            }
            _context.FileUploadTable.AddRange(file);
            CommitTransaction();
            NidPhotoIdentity(portal.PortalLoanSummaryID, GuarantorPhoto.FileUploadId ,GuarantorNID.FileUploadId);
        }

        public IEnumerable<PortalLoanSummary> GetAllPortalLoanSummary()
        {
            return _context.PortalLoanSummary;
        }
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

        public async Task<IEnumerable<PortalLoanSummary>> getByLoanStatus(byte type, long memberId)
        {
            return _context.PortalLoanSummary.Where(t => t.LoanStatus == type && t.MemberID == memberId);
        }

        public void NidPhotoIdentity(long PortalLoanSummaryId,long Photo,long NID)
        {
            BeginTransaction();
            var portalLoanSummary = GetById(PortalLoanSummaryId);
            if(portalLoanSummary != null)
            {
                portalLoanSummary.GuarantorImg = Photo;
                portalLoanSummary.GuarantorNID = NID;
            }
            CommitTransaction();

        }
    }
}
