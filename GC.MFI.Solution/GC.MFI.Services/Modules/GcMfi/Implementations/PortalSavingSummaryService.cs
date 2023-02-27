﻿using AutoMapper;
using GC.MFI.DataAccess.InfrastructureBase;
using GC.MFI.DataAccess.Repository.Interfaces;
using GC.MFI.Models;
using GC.MFI.Models.DbModels;
using GC.MFI.Models.Models;
using GC.MFI.Models.RequestModels;
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
    public class PortalSavingSummaryService : LegacyServiceBase<PortalSavingSummary>, IPortalSavingSummaryService
    {
        private readonly IPortalSavingSummaryRepository _repository;
        private readonly IFileUploadService _uploadService;
        private readonly INomineeXPortalSavingSummaryService _nominee;
        private readonly IMapper mapper;
        public PortalSavingSummaryService(IPortalSavingSummaryRepository repository,INomineeXPortalSavingSummaryService nominee, IFileUploadService fService, IUnitOfWork unitOfWork, IMapper _mapper) : base(repository, unitOfWork, _mapper)
        {
            this._repository = repository;
            this._uploadService = fService;
            this._nominee = nominee;
            this.mapper = _mapper;
        }

        //public async Task<PortalSavingSummary> Create(PortalSavingSummary request)
        //{
        //    var model = await _repository.Create(request);
        //    return model;
        //}

        public void CreatePortalSavingSummary(PortalSavingSummaryFileUpload entity)
        {
            try
            {
                _repository.BeginTransaction();
                var model = new PortalSavingSummary()
                {
                    PortalSavingSummaryID = entity.PortalSavingSummaryID,
                    OfficeID = entity.OfficeID,
                    MemberID = entity.MemberID,
                    ProductID = entity.ProductID,
                    CenterID = entity.CenterID,
                    NoOfAccount = entity.NoOfAccount,
                    TransactionDate = entity.TransactionDate,
                    Deposit = entity.Deposit,
                    Withdrawal = entity.Withdrawal,
                    Balance = entity.Balance,
                    InterestRate = entity.InterestRate,
                    SavingInstallment = entity.SavingInstallment,
                    CumInterest = entity.CumInterest,
                    MonthlyInterest = entity.MonthlyInterest,
                    Penalty = entity.Penalty,
                    OpeningDate = entity.OpeningDate,
                    MaturedDate = entity.MaturedDate,
                    ClosingDate = entity.ClosingDate,
                    TransType = entity.TransType,
                    SavingStatus = 1,
                    Posted = entity.Posted,
                    IsActive = entity.IsActive,
                    InActiveDate = entity.InActiveDate,
                    Duration = entity.Duration,
                    InstallmentNo = entity.InstallmentNo,
                    CreateDate = entity.CreateDate,
                    CreateUser = entity.CreateUser,
                    SavingAccountNo = entity.SavingAccountNo,
                    Ref_EmployeeID = entity.Ref_EmployeeID,
                    ApprovalStatus = false
                };
                _repository.Add(model);
                Save();
                int count = entity.MemberNomines.Count;
                FileUploadTable[] nomineeImage = new FileUploadTable[count];
                FileUploadTable[] nomineeNID = new FileUploadTable[count];
                
                FileUploadTable[] list = new FileUploadTable[count + count];
                int j = 0;
                for (int i = 0; i < entity.MemberNomines.Count; i++)
                {
                    Base64File Nimg = ImageHelper.GetFileDetails(entity.MemberNomines[i].Image);
                    nomineeImage[i] = new FileUploadTable
                    {
                        EntityId = model.PortalSavingSummaryID,
                        EntityName = "PortalSavingSummary",
                        PropertyName = "NomineeImage",
                        FileName = $"Nominee_Image_{entity.MemberNomines[i].NIDNumber}",
                        File = Nimg.DataBytes,
                        Type = Nimg.MimeType,
                        DocumentType = "NomineeImage"
                    };
                    
                    list[j] = nomineeImage[i];
                    j++;
                    Base64File NNID = ImageHelper.GetFileDetails(entity.MemberNomines[i].Nid);
                    nomineeNID[i] = new FileUploadTable
                    {
                        EntityId = model.PortalSavingSummaryID,
                        EntityName = "PortalSavingSummary",
                        PropertyName = "NomineeNID",
                        FileName = $"Nominee_NID_{entity.MemberNomines[i].NIDNumber}",
                        File = NNID.DataBytes,
                        Type = NNID.MimeType,
                        DocumentType = "NomineeNID"
                    };
                    list[j] = nomineeNID[i];
                    j++;
                }

                _uploadService.BulkCreate(list);

                NomineeXPortalSavingSummary[] NomineeXSaving = new NomineeXPortalSavingSummary[entity.MemberNomines.Count];
                for (int i = 0; i < NomineeXSaving.Length; i++)
                {
                    var img = _uploadService.Get(t => t.EntityId == model.PortalSavingSummaryID
                        && t.PropertyName == "NomineeImage" &&
                        t.FileName == $"Nominee_Image_{entity.MemberNomines[i].NIDNumber}");

                    var Nid = _uploadService.Get(t => t.EntityId == model.PortalSavingSummaryID
                        && t.PropertyName == "NomineeNID" &&
                        t.FileName == $"Nominee_NID_{entity.MemberNomines[i].NIDNumber}");

                    NomineeXSaving[i] = new NomineeXPortalSavingSummary
                    {
                        PortalSavingSummaryID = model.PortalSavingSummaryID,
                        NomineeName = entity.MemberNomines[i].NomineeName,
                        NFatherName = entity.MemberNomines[i].NFatherName,
                        NAddressName = entity.MemberNomines[i].NAddressName,
                        NRelationName = entity.MemberNomines[i].NRelationName,
                        NAlocation = entity.MemberNomines[i].NAlocation,
                        NIDNumber = entity.MemberNomines[i].NIDNumber,
                        ImageId = img.FileUploadId,
                        NIDId = Nid.FileUploadId
                    };
                }
                _nominee.BulkCreate(NomineeXSaving);

                if (entity.PortalSavingFileUpload != null)
                {
                    // BULT INSERT DATA
                    FileUploadTable[] file = new FileUploadTable[entity.PortalSavingFileUpload.Count];
                    for (int i = 0; i < entity.PortalSavingFileUpload.Count(); i++)
                    {
                        Base64File filesType = ImageHelper.GetFileDetails(entity.PortalSavingFileUpload[i].File);
                        file[i] = new FileUploadTable
                        {

                            EntityId = model.PortalSavingSummaryID,
                            EntityName = "PortalSavingSummary",
                            PropertyName = "SupportingDocument",
                            FileName = $"SupportingDocument_L{model.PortalSavingSummaryID}_{i + 1}",
                            Type = filesType.MimeType,
                            File = filesType.DataBytes,
                            DocumentType = entity.PortalSavingFileUpload[i].DocumentType,
                        };

                    }
                    _uploadService.BulkCreate(file);
                    SupportingDocumentIdentity(model.PortalSavingSummaryID);

                }
               _repository.CommitTransaction();

            }
            catch(Exception ex)
            {
                _repository.RollbackTransaction();
                throw ex;
            }
        }

        public async Task<PagedResponse<IEnumerable<SavingSummaryViewModel>>> GetAllPortalSavingSummaryPaged(PaginationFilter<SavingSummaryViewModel> filter, long Id)
        {
            var result = await _repository.GetAllPortalSavingSummaryPaged(filter, Id);
            return result;
        }

        public Task<IEnumerable<SavingSummaryViewModel>> getBySavingStatus(byte type, long memberId)
        {
            var getSavingStatus = _repository.getBySavingStatus(type, memberId);
            return getSavingStatus;
        }
        public async Task<PortalSavingSummaryViewModel> PortalSavingSummaryDetails(long Id)
        {
            try
            {
                PortalSavingSummary savingSummary = GetById(Id);
                var map = mapper.Map<PortalSavingSummaryViewModel>(savingSummary);

                FileUploadTable[] savingSummaries = _uploadService.GetMany(t => t.EntityId == Id ).ToArray();

                var NomineeMap = mapper.Map<NomineeXPortalSavingSummaryViewModel[]>(map.MemberNomines);
 
               for(int i = 0; i < NomineeMap.Length; i++)
                {
                    var NomineeImg = savingSummaries.Where(t => t.FileUploadId == NomineeMap[i].ImageId).FirstOrDefault();
                    var NomineeNID = savingSummaries.Where(t => t.FileUploadId == NomineeMap[i].NIDId).FirstOrDefault();
                    NomineeMap[i].Nid = FileDecodeHelper.Base64(NomineeNID.Type, NomineeNID.File);
                    NomineeMap[i].Image = FileDecodeHelper.Base64(NomineeImg.Type, NomineeImg.File);
                }
                map.MemberNomines = NomineeMap;
                if (!String.IsNullOrEmpty(map.SupportingDocumentsId))
                {
                    FileUploadTable[] savingSummaryFile = savingSummaries.Where(t => t.PropertyName == "SupportingDocument").ToArray();
                    var fileMap = mapper.Map<FileUploadTableViewModel[]>(savingSummaryFile);

                    for (int i = 0; i < fileMap.Length; i++)
                    {
                      fileMap[i].FileUrl = FileDecodeHelper.Base64(savingSummaryFile[i].Type, savingSummaryFile[i].File);
                    }
                    map.PortalSavingFileUpload = fileMap;
                }
                return map;
            }
            catch(Exception ex)
            {
                throw ex;
            }
        }

        #region Helper function

        public void SupportingDocumentIdentity(long PortalSavingId)
        {
            var getSupportingDocument = _uploadService.GetMany(t => t.EntityId == PortalSavingId && t.PropertyName == "SupportingDocument").ToList();
            long[] SD = new long[getSupportingDocument.Count];
            for (int i = 0; i < getSupportingDocument.Count(); i++)
            {
                SD[i] = getSupportingDocument[i].FileUploadId;
            }
            var SDID = string.Join(",", SD);
            var getPortalSavingSummary = GetById(PortalSavingId);
            getPortalSavingSummary.SupportingDocumentsId = SDID;
            _uploadService.Save();
        }
        #endregion

    }
}
