using GC.MFI.Models.DbModels;
using GC.MFI.Models.ViewModels;
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
        Task<Member> UpdateMemberProfile(MemberProfileUpdate memberProfile);

        Task<Member> GetMemberByPortalId(long portalMemberId);

        Task<string> GetImageByMemberID(long memberId);
        Task<string> UpdateMemberImage(string image,long memberId);
    }
}
