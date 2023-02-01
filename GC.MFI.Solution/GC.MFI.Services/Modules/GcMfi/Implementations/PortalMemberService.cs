using AutoMapper;
using GC.MFI.DataAccess.InfrastructureBase;
using GC.MFI.DataAccess.Repository.Interfaces;
using GC.MFI.Models.DbModels;
using GC.MFI.Models.ViewModels;
using GC.MFI.Services.Modules.GcMfi.Interfaces;
using GC.MFI.Services.Modules.Security.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GC.MFI.Services.Modules.GcMfi.Implementations
{
    public class PortalMemberService : ServiceBase<PortalMemberViewModel, PortalMember>, IPortalMemberService
    {
        private readonly IPortalMemberRepository _repository;
        public PortalMemberService(IPortalMemberRepository repository, IUnitOfWork unitOfWork, IMapper _mapper) : base(repository, unitOfWork, _mapper)
        {
            this._repository = repository;
        }

        public async Task<MemberProfile> GetMemberById(long Id)
        {
            var member = await _repository.GetMemberById(Id);
            return member;
        }
    }
}
