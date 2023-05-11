using AutoMapper;
using GC.MFI.DataAccess.InfrastructureBase;
using GC.MFI.DataAccess.Repository.Interfaces;
using GC.MFI.Models;
using GC.MFI.Models.DbModels;
using GC.MFI.Models.ViewModels;
using GC.MFI.Services.Modules.GcMfi.Interfaces;
using GC.MFI.Utility.Helpers;
using Org.BouncyCastle.Crypto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GC.MFI.Services.Modules.GcMfi.Implementations
{
    public class SavingSummaryService : LegacyServiceBase<SavingSummary>, ISavingSummaryService
    {
        private ISavingSummaryRepository _repository;
        private readonly IFileUploadRepository _repositoryFile;
        private IMapper _mapper;
        public SavingSummaryService(ISavingSummaryRepository repository, IFileUploadRepository _repositoryFile, IUnitOfWork unitOfWork, IMapper _mapper) : base(repository, unitOfWork, _mapper)
        {
            _repository = repository;
            this._mapper = _mapper;
            this._repositoryFile = _repositoryFile;
        }

        public PagedResponse<IQueryable<SavingsSummaryViewModel>> GetAllPortalSavingSummaryPaged(PaginationFilter<SavingsSummaryViewModel> filter, long Id)
        {
            var result =  _repository.GetAllPortalSavingSummaryPaged(filter, Id);
            return result;
        }

        public SavingsSummaryViewModel SavingSummaryDetails(long Id)
        {
            var GetSaving = _repository.GetById(Id);
            var map = _mapper.Map<SavingsSummaryViewModel>(GetSaving);
            var nominee = _repository.GetNominess(Id);
            var nomineeMap = _mapper.Map<List<NomineeXSavingsSummaryViewModel>>(nominee);
            map.MemberNomines = nomineeMap;
            for (int i = 0; i < map.MemberNomines.Count; i++)
            {
                var NomineeImg = _repositoryFile.GetById(map.MemberNomines[i].ImageId);
                var NomineeNID = _repositoryFile.GetById(map.MemberNomines[i].NIDId);
                map.MemberNomines[i].NID = FileDecodeHelper.Base64(NomineeNID.Type, NomineeNID.File);
                map.MemberNomines[i].Image = FileDecodeHelper.Base64(NomineeImg.Type, NomineeImg.File);
            }
            if (!String.IsNullOrEmpty(map.SupportingDocumentsId))
            {
                string[] numbers = map.SupportingDocumentsId.Split(',');
                long[] ids = Array.ConvertAll(numbers, s => long.Parse(s));
                var supportingdocument = _repositoryFile.GetMany(t=> ids.Contains(t.FileUploadId)).ToList();

                var fileMap = _mapper.Map<FileUploadTableViewModel[]>(supportingdocument);

                for (int i = 0; i < fileMap.Length; i++)
                {
                    fileMap[i].FileUrl = FileDecodeHelper.Base64(supportingdocument[i].Type, supportingdocument[i].File);
                }
                map.FileUploadTables = fileMap;
            }


            return map;
        }
    }
}
