using GC.MFI.DataAccess.InfrastructureBase;
using GC.MFI.Models.DbModels;
using GC.MFI.Models.ViewModels;
using System.Threading.Tasks;

namespace GC.MFI.DataAccess.Repository.Interfaces
{
    public interface IPortalMemberRepository : IRepository<PortalMember>
    {
        Task<PortalMember> CreatePortalMember(SignUpModel signUp);
        void CreatePortalMemberNID(long portalMemberId, long portalMemberFId);
        void CreatePortalMemberImage(long portalMemberId, long portalMemberIId);
        Task<MemberProfile> GetMemberById(long Id);
    }
}
