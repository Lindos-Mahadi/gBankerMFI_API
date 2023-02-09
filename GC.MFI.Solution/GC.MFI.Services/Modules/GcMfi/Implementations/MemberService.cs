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
        private readonly IUnitOfWork _unitOfWork;

        public MemberService(IMemberRepository repository, IUnitOfWork unitOfWork, IMapper _mapper) : base(repository, unitOfWork, _mapper)
        {
            _repository = repository;
            _unitOfWork = unitOfWork;
        }

        public async Task<IEnumerable<Member>> GetAllMember(string search)
        {
            var memberList = await _repository.GetAllMember(search);
            return memberList;
        }

        public async Task<Member> UpdateMemberProfile(Member memberProfile)
        {
            var dbMember = _repository.GetById(memberProfile.MemberID);
            ///map only the updated fields
            ///

            _repository.Update(dbMember);
            _unitOfWork.Commit();

           // var updateMember = await _repository.UpdateMemberProfile(memberProfile);
            return dbMember;
;
        }
        public async Task<Member> GetMemberByPortalId(long portalMemberId)
        {
            var memeber = await _repository.GetMemberByPortalId(portalMemberId);
            return memeber;
        }
        public async Task<string> GetImageByMemberID(long memberId)
        {
            return await _repository.GetImageByMemberID(memberId);
        }
    }
}
