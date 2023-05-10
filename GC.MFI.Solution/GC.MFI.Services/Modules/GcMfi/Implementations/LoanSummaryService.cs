using AutoMapper;
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
    public class LoanSummaryService : LegacyServiceBase<LoanSummary>, ILoanSummaryService
    {
        private readonly ILoanSummaryRepository _repository;
        private readonly IFileUploadService _fileService; 
        private readonly IMapper _mapper;
        public LoanSummaryService(ILoanSummaryRepository repository, IFileUploadService _fileService, IUnitOfWork unitOfWork, IMapper _mapper) : base(repository, unitOfWork, _mapper)
        {
            _repository = repository;
            this._mapper = _mapper;
            this._fileService = _fileService;
        }

        public PagedResponse<IQueryable<LoanSummaryViewModel>> GetAllPortalLoanSummaryPaged(PaginationFilter<LoanSummaryViewModel> filter, long Id)
        {
            var response = _repository.GetAllPortalLoanSummaryPaged(filter, Id);
            return response;
        }
        public LoanSummaryViewModel GetById(long id)
        {
            var GetLoan = _repository.GetById(id);
            var GetGuaranntorImage = _fileService.GetById(GetLoan.GuarantorImg ?? 0);
            var GetGuranntorNId = _fileService.GetById(GetLoan.GuarantorNID ?? 0);
           string[] numbers = !String.IsNullOrEmpty(GetLoan.SupportingDocumentsId) ? GetLoan.SupportingDocumentsId.Split(',') : null;
            
            long[] ids = null;
            if (numbers != null)
            {
               ids = Array.ConvertAll(numbers, s => long.Parse(s));
            }
            long[] ids2 = {0};
            var supportingdocument = _fileService.GetByMultipleId(ids != null ? ids : ids2);
                FileUploadTableViewModel[] list = new FileUploadTableViewModel[supportingdocument.Count()];
                for (int i = 0; i < supportingdocument.Count(); i++)
                {
                    list[i] = new FileUploadTableViewModel
                    {
                        FileUploadId = supportingdocument[i].FileUploadId,
                        FileName = supportingdocument[i].FileName,
                        EntityId = supportingdocument[i].EntityId,
                        EntityName = supportingdocument[i].EntityName,
                        PropertyName = supportingdocument[i].PropertyName,
                        DocumentType = supportingdocument[i].DocumentType,
                        FileUrl = FileDecodeHelper.Base64(supportingdocument[i].Type, supportingdocument[i].File),

                    };
                }
            string GImgUrl = GetGuaranntorImage != null? FileDecodeHelper.Base64(GetGuaranntorImage.Type, GetGuaranntorImage.File) : null;
            string GNidUrl = GetGuranntorNId != null ? FileDecodeHelper.Base64(GetGuranntorNId.Type, GetGuranntorNId.File) : null;
            return new LoanSummaryViewModel
            {
                LoanSummaryID = GetLoan.LoanSummaryID,
                ImageUrl = GImgUrl,
                NidUrl = GNidUrl,
                FileUploads = list,
            };

        }
    }
}
