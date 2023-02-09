using AutoMapper;
using GC.MFI.DataAccess;
using GC.MFI.DataAccess.InfrastructureBase;
using GC.MFI.DataAccess.Repository.Interfaces;
using GC.MFI.Models;
using GC.MFI.Models.DbModels;
using GC.MFI.Models.ViewModels;
using GC.MFI.Services.Modules.GcMfi.Interfaces;
using GC.MFI.Utility.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GC.MFI.Services.Modules.GcMfi.Implementations
{
    public class PortalLoanSummaryService : LegacyServiceBase<PortalLoanSummary>, IPortalLoanSummaryService
    {
        private readonly IPortalLoanSummaryRepository _repository;

        private readonly IFileUploadService _fileService;
        public PortalLoanSummaryService(IPortalLoanSummaryRepository repository, IFileUploadService fileService, IUnitOfWork unitOfWork, IMapper _mapper) : base(repository, unitOfWork, _mapper)
        {
            _repository = repository;
            _fileService = fileService;
        }

        public void CreatePortalLoanSummary(PortalLoanSummaryFileUpload entity)
        {
            try
            {
                _repository.BeginTransaction();
                var portal = new PortalLoanSummary
                {
                    PortalLoanSummaryID = entity.PortalLoanSummaryID,
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
                    LoanStatus = 1,
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
                    ApprovalStatus = false
                };
                Create(portal);
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
                _fileService.Create(GuarantorNID);
                _fileService.Create(GuarantorPhoto);

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
                _fileService.BulkCreate(file);
                NidPhotoIdentity(portal.PortalLoanSummaryID, GuarantorPhoto.FileUploadId, GuarantorNID.FileUploadId);
                SupportingDocumentIdentity(portal.PortalLoanSummaryID);
                _repository.CommitTransaction();
            }
            catch(Exception ex)
            {
                _repository.RollbackTransaction();
                throw ex;
            }
        }

        public virtual IEnumerable<PortalLoanSummary> GetAllPortalLoanSummary()
        {
            var result = _repository.GetAll();
            return result;
        }

        public async Task<PagedResponse<IEnumerable<PortalLoanSummaryViewModel>>> GetAllPortalLoanSummaryPaged(PaginationFilter<PortalLoanSummaryViewModel> filter, long Id)
        {
            var result = await _repository.GetAllPortalLoanSummaryPaged(filter, Id);
            return result;
        }

        public Task<IEnumerable<PortalLoanSummaryViewModel>> getByLoanStatus(byte type, long memberId)
        {
            var getLoanStatus = _repository.getByLoanStatus(type, memberId);
            return getLoanStatus;
        }


        public void NidPhotoIdentity(long PortalLoanSummaryId, long Photo, long NID)
        {
            var portalLoanSummary = GetById(PortalLoanSummaryId);
            if (portalLoanSummary != null)
            {
                portalLoanSummary.GuarantorImg = Photo;
                portalLoanSummary.GuarantorNID = NID;
                Save();
            }
        }

        public void SupportingDocumentIdentity(long PortalLoanId)
        {
            var getSupportingDocument = _fileService.GetMany(t => t.EntityId == PortalLoanId && t.PropertyName == "SupportingDocument").ToList();
            long[] SD = new long[getSupportingDocument.Count];
            for (int i = 0; i < getSupportingDocument.Count(); i++)
            {
                SD[i] = getSupportingDocument[i].FileUploadId;
            }
            var SDID = string.Join(",", SD);
            var getPortalLoanSummary = GetById(PortalLoanId);
            getPortalLoanSummary.SupportingDocumentsId = SDID;
            Save();
        }
    }
}
