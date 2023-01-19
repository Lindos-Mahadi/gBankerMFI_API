using AutoMapper;
using GC.MFI.DataAccess.InfrastructureBase;
using GC.MFI.DataAccess.Repository.Implementations;
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
    public class PurposeService : LegacyServiceBase<Purpose>, IPurposeService
    {
        private readonly IPurposeRepository _repository;
        public PurposeService(IPurposeRepository repository, IUnitOfWork unitOfWork, IMapper _mapper) 
            : base(repository, unitOfWork, _mapper)
        {
            this._repository = repository;
        }

        public async  Task<IEnumerable<Purpose>> GetAllPurpose(string search)
        {
            var purposeList = await _repository.GetAllPurpose(search);
            return purposeList;
        }
    }
}
