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
    public class UnionService : LegacyServiceBase<Union>, IUnionService
    {
        private readonly IUnionRepository _repository;
        public UnionService(IUnionRepository repository, IUnitOfWork unitOfWork, IMapper _mapper) : base(repository, unitOfWork, _mapper)
        {
            this._repository = repository;
        }

        public async Task<IEnumerable<Union>> GetAllUnionName(string search)
        {
            var unionList = await  _repository.GetAllUnionName(search);
            return unionList;
        }
    }
}
