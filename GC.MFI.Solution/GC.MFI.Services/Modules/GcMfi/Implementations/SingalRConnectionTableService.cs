using AutoMapper;
using GC.MFI.DataAccess.InfrastructureBase;
using GC.MFI.DataAccess.Repository.Interfaces;
using GC.MFI.Models.DbModels;
using GC.MFI.Services.Modules.GcMfi.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GC.MFI.Services.Modules.GcMfi.Implementations
{
    public class SingalRConnectionTableService : LegacyServiceBase<SingalRConnectionTable> , ISingalRConnectionTableService
    {
        private readonly ISingalRConnectionTableRepository _repository;
        private readonly IMapper _mapper;
        public SingalRConnectionTableService(ISingalRConnectionTableRepository repository, IUnitOfWork unitOfWork, IMapper _mapper) : base(repository, unitOfWork, _mapper)
        {
            _repository = repository;
            this._mapper = _mapper;

        }
    }
}
