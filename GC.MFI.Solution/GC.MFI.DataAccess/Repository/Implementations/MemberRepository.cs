using GC.MFI.DataAccess.InfrastructureBase;
using GC.MFI.DataAccess.Repository.Interfaces;
using GC.MFI.Models.DbModels;
using GC.MFI.Models.ViewModels;
using GC.MFI.Utility.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GC.MFI.DataAccess.Repository.Implementations
{
    public class MemberRepository : LegacyRepositoryBase<Member>, IMemberRepository
    {
        private readonly GBankerDbContext _context;
        public MemberRepository(IDatabaseFactory databaseFactory) : base(databaseFactory)
        {
        }
        public void  UpdateMemberProfile(MemberProfileUpdate memberProfile)
        {
            DataContext.Update(memberProfile);
        }
        public async Task<IEnumerable<Member>> GetAllMember(string search)
        {
            if (!String.IsNullOrEmpty(search))
            {
                var memberList = DataContext.Member.Where(t => t.FirstName!.Contains(search) || t.FirstName.Trim()
                .Replace(" ", "").ToUpper()!.Contains(search.Trim().Replace(" ", "").ToUpper()));
                return memberList.Skip(0).Take(10);

            }
            return DataContext.Member.Skip(0).Take(10);
        }

        public async Task<Member> GetMemberByPortalId(long portalMemberId)
        {
            var member = DataContext.Member.Where(t => t.PortalMemberId == portalMemberId).FirstOrDefault();
            return member;
        }

        public async Task<string> GetImageByMemberID(long memberId)
        {
            Member getMember = GetById(memberId);
            if(getMember?.Image == null)
            {
                return null;
            }
            FileUploadTable Image = DataContext.FileUploadTable.Find(getMember.Image);
            string ToBase64 = FileDecodeHelper.Base64(Image.Type , Image.File);
            return ToBase64;
        }

        
    }
}
