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
            if(!String.IsNullOrEmpty(GetLoan.SupportingDocumentsId))
            {
                string[] numbers = GetLoan.SupportingDocumentsId.Split(',');
                long[] ids = Array.ConvertAll(numbers, s => long.Parse(s));
                var supportingdocument = _fileService.GetByMultipleId(ids);
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
                string GImgUrl = FileDecodeHelper.Base64(GetGuaranntorImage.Type, GetGuaranntorImage.File);
                string GNidUrl = FileDecodeHelper.Base64(GetGuranntorNId.Type, GetGuranntorNId.File);
                return new LoanSummaryViewModel
                {
                    LoanSummaryID = GetLoan.LoanSummaryID,
                    ImageUrl = GImgUrl,
                    NidUrl = GNidUrl,
                    FileUploads = list,
                };
            }
            return null;
           
        }
    }
}
