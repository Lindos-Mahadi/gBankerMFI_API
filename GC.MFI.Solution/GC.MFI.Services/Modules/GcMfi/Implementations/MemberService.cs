using AutoMapper;
using GC.MFI.DataAccess;
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
    public class MemberService : LegacyServiceBase<Member>, IMemberService
    {
        private readonly IMemberRepository _repository;
        public MemberService(IMemberRepository repository, IUnitOfWork unitOfWork, IMapper _mapper) : base(repository, unitOfWork, _mapper)
        {
            _repository = repository;
        }

        public async Task<IEnumerable<Member>> GetAllMember(string search)
        {
            var memberList = await _repository.GetAllMember(search);
            return memberList;
        }

        public async Task<Member> UpdateMember(Member member)
        {
            var updateMember = await _repository.UpdateMember(member);
            return updateMember;
        }
        public async Task<Member> GetMemberByPortalId(long portalMemberId)
        {
            var memeber = await _repository.GetMemberByPortalId(portalMemberId);
            return memeber;
        }
    }
}
