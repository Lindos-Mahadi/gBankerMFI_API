using AutoMapper;
using GC.MFI.DataAccess.InfrastructureBase;
using GC.MFI.DataAccess.Repository.Interfaces;
using GC.MFI.Models.DbModels;
using GC.MFI.Models.RequestModels;
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

        public async Task<PortalSavingSummary> Create(SavingAccountModel request)
        {
            var model = await _repository.Create(request);
            return model;
        }
    }
}
