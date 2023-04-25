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
    public class SignalRConnectionTableService : LegacyServiceBase<SignalRConnectionTable> , ISignalRConnectionTableService
    {
        private readonly ISignalRConnectionTableRepository _repository;
        private readonly IMapper _mapper;
        public SignalRConnectionTableService(ISignalRConnectionTableRepository repository, IUnitOfWork unitOfWork, IMapper _mapper) : base(repository, unitOfWork, _mapper)
        {
            _repository = repository;
            this._mapper = _mapper;

        }
    }
}
