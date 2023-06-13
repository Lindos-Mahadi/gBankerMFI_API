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
        private readonly IMapper mapper;
        public PortalLoanSummaryService(IPortalLoanSummaryRepository repository, IFileUploadService fileService, IUnitOfWork unitOfWork, IMapper _mapper) : base(repository, unitOfWork, _mapper)
        {
            _repository = repository;
            _fileService = fileService;
            this.mapper = _mapper;
        }

        public override PortalLoanSummary Create(PortalLoanSummary objectToCreate)
        {
            return base.Create(objectToCreate);
        }
        public PortalLoanSummary CreatePortalLoanSummary(PortalLoanSummaryFileUpload entity)
        {
            try
            {
                _repository.BeginTransaction();
                entity.LoanStatus = 1;
                entity.ApprovalStatus = false;
                var portal = mapper.Map<PortalLoanSummary>(entity);
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
                    if(filesTypes == null) 
                    {
                        _repository.RollbackTransaction();
                        return null; 
                    }
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
                portal.GuarantorImgId =  createdList[1].FileUploadId;
                portal.GuarantorNIDId = createdList.First().FileUploadId;
                var supportingDocIds = String.Join(",", createdList.Where(t=> t.PropertyName == "SupportingDocument").Select(s => s.FileUploadId).ToArray());
                portal.SupportingDocumentsId = supportingDocIds;
             
                _repository.CommitTransaction();
                return portal;
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

        public Task<IEnumerable<PortalLoanSummaryViewModel>> getByLoanStatus(long memberId)
        {
            var getLoanStatus = _repository.getByLoanStatus(memberId);
            return getLoanStatus;
        }

        public PortalLoanSummaryViewModel GetById(long id)
        {
            var GetLoan = _repository.GetById(id);
            var GetGuaranntorImage = _fileService.GetById(GetLoan.GuarantorImgId);
            var GetGuranntorNId = _fileService.GetById(GetLoan.GuarantorNIDId);

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
