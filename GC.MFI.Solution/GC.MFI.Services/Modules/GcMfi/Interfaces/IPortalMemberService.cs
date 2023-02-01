using GC.MFI.Models.DbModels;
using GC.MFI.Models.ViewModels;
using System.Threading.Tasks;

namespace GC.MFI.Services.Modules.GcMfi.Interfaces
{
    public interface IPortalMemberService : IServiceBase<PortalMemberViewModel, PortalMember>
    {
        Task<MemberProfile> GetMemberById(long Id);
    }
}
