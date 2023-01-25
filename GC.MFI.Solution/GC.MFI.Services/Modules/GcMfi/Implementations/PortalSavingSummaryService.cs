using AutoMapper;
using GC.MFI.DataAccess.InfrastructureBase;
using GC.MFI.DataAccess.Repository.Interfaces;
using GC.MFI.Models;
using GC.MFI.Models.DbModels;
using GC.MFI.Models.RequestModels;
using GC.MFI.Models.ViewModels;
using GC.MFI.Services.Modules.GcMfi.Interfaces;
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
        public PortalSavingSummaryService(IPortalSavingSummaryRepository repository, IUnitOfWork unitOfWork, IMapper _mapper) : base(repository, unitOfWork, _mapper)
        {
            this._repository = repository;
        }

        public async Task<PortalSavingSummary> Create(PortalSavingSummary request)
        {
            var model = await _repository.Create(request);
            return model;
        }

        public async Task<PagedResponse<IEnumerable<SavingSummaryViewModel>>> GetAllPortalSavingSummaryPaged(PaginationFilter<SavingSummaryViewModel> filter)
        {
            var result = await _repository.GetAllPortalSavingSummaryPaged(filter);
            return result;
        }
    }
}
