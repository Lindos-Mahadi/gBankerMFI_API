using AutoMapper;
using GC.MFI.DataAccess.InfrastructureBase;
using GC.MFI.DataAccess.Repository.Interfaces;
using GC.MFI.Models;
using GC.MFI.Models.DbModels;
using GC.MFI.Models.ViewModels;
using GC.MFI.Services.Modules.GcMfi.Interfaces;
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
        private IMapper _mapper;
        public SavingSummaryService(ISavingSummaryRepository repository, IUnitOfWork unitOfWork, IMapper _mapper) : base(repository, unitOfWork, _mapper)
        {
            _repository = repository;
            this._mapper = _mapper;
        }

        public PagedResponse<IQueryable<SavingsSummaryViewModel>> GetAllPortalSavingSummaryPaged(PaginationFilter<SavingsSummaryViewModel> filter, long Id)
        {
            var result =  _repository.GetAllPortalSavingSummaryPaged(filter, Id);
            return result;
        }
    }
}
