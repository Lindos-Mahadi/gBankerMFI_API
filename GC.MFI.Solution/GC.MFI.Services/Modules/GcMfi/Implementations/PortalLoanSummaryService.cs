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

        public override PortalLoanSummary Create(PortalLoanSummary objectToCreate)
        {
            return base.Create(objectToCreate);
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
                _repository.Add(portal);
                Save();

                // For GuarantorNID & image
                 var fileUplods = new List<PortalLoanFileUpload>();
                fileUplods.Add(new PortalLoanFileUpload { PropertyName = "GuarantorNID", File = entity.GuarantorNID, FileName = $"GuarantorNID_{portal.PortalLoanSummaryID}" });
                fileUplods.Add(new PortalLoanFileUpload { PropertyName = "GuarantorImage", File = entity.GuarantorImg, FileName = $"GuarantorImage_{portal.PortalLoanSummaryID}" });
                fileUplods.AddRange(entity.PortalLoanFileUpload);
                // For bulk insert
                FileUploadTable[] file = new FileUploadTable[fileUplods.Count()];
                for (int i = 0; i < fileUplods.Count(); i++)
                {
                    var fileToUpload = fileUplods[i];
                    Base64File filesTypes = ImageHelper.GetFileDetails(fileToUpload.File);
                    file[i] = new FileUploadTable
                    {
                        EntityId = portal.PortalLoanSummaryID,
                        EntityName = "PortalLoanSummary",
                        PropertyName = string.IsNullOrEmpty(fileToUpload.PropertyName) ? "SupportingDocument" : fileToUpload.PropertyName,
                        FileName = i == 0 ? $"GuarantorNID_{portal.PortalLoanSummaryID}" : i == 1 ? $"GuarantorImage_{portal.PortalLoanSummaryID}" : $"SupportingDocument_L{portal.PortalLoanSummaryID}_{i - 1}",
                        Type = filesTypes.MimeType,
                        File = filesTypes.DataBytes,
                        DocumentType = fileToUpload.DocumentType
                    };
                }
                var createdList = _fileService.BulkCreate(file);
                portal.GuarantorImg =  createdList[1].FileUploadId;
                portal.GuarantorNID = createdList.First().FileUploadId;
                var supportingDocIds = String.Join(",", createdList.Where(t=> t.EntityName == "SupportingDocument").Select(s => s.FileUploadId).ToArray());
                portal.SupportingDocumentsId = supportingDocIds;
             
                _repository.CommitTransaction();
            }
            catch (Exception ex)
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

        public PortalLoanSummaryViewModel GetById(long id)
        {
            var GetLoan = _repository.GetById(id);
            var GetGuaranntorImage = _fileService.GetById(GetLoan.GuarantorImg);
            var GetGuranntorNId = _fileService.GetById(GetLoan.GuarantorNID);

            var supportingDoc = _fileService.GetMany(t => t.EntityId == id && t.PropertyName == "SupportingDocument").ToList();
            FileUploadTableViewModel[] list = new FileUploadTableViewModel[supportingDoc.Count()];
            
            for(int i = 0; i < supportingDoc.Count(); i++)
            {
                list[i] = new FileUploadTableViewModel
                {
                    FileUploadId = supportingDoc[i].FileUploadId,
                    FileName = supportingDoc[i].FileName,
                    EntityId = supportingDoc[i].EntityId,
                    EntityName = supportingDoc[i].EntityName,
                    PropertyName = supportingDoc[i].PropertyName,
                    DocumentType = supportingDoc[i].DocumentType,
                    FileUrl = FileDecodeHelper.Base64(supportingDoc[i].Type, supportingDoc[i].File),

                };
            }

            string GImgUrl = FileDecodeHelper.Base64(GetGuaranntorImage.Type, GetGuaranntorImage.File);
            string GNidUrl = FileDecodeHelper.Base64(GetGuranntorNId.Type, GetGuranntorNId.File);


            return new PortalLoanSummaryViewModel
            {
                PortalLoanSummaryID = GetLoan.PortalLoanSummaryID,
                ImageUrl = GImgUrl,
                NidUrl = GNidUrl,
                FileUploads = list,
            };
            
        }
    }
}
