using AutoMapper;
using GC.MFI.DataAccess;
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
    public class PortalLoanSummaryService : LegacyServiceBase<PortalLoanSummary>, IPortalLoanSummaryService
    {
        private readonly IPortalLoanSummaryRepository _repository;
        public PortalLoanSummaryService(IPortalLoanSummaryRepository repository, IUnitOfWork unitOfWork, IMapper _mapper) : base(repository, unitOfWork, _mapper)
        {
            _repository = repository;
        }

        public void CreatePortalLoanSummary(PortalLoanSummaryFileUpload entity)
        {
            _repository.CreatePortalLoanSummary(entity);
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

        public Task<IEnumerable<PortalLoanSummary>> getByLoanStatus(byte type, long memberId)
        {
            var getLoanStatus = _repository.getByLoanStatus(type, memberId);
            return getLoanStatus;
        }
    }
}
