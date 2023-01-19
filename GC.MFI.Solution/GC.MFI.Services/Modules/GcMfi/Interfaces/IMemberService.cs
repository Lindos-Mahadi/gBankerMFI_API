using GC.MFI.Models.DbModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GC.MFI.Services.Modules.GcMfi.Interfaces
{
    public interface IMemberService : ILegacyServiceBase<Member>
    {
        Task<IEnumerable<Member>> GetAllMember(string search);
        Task<Member> UpdateMember(Member member);

        Task<Member> GetMemberByPortalId(long portalMemberId);
    }
}
