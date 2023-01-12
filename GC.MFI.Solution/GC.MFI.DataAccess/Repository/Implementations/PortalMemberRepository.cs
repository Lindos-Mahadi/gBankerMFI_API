using GC.MFI.DataAccess.InfrastructureBase;
using GC.MFI.DataAccess.Repository.Interfaces;
using GC.MFI.DataAccess.Repository.Interfaces.Security;
using GC.MFI.Models.DbModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GC.MFI.DataAccess.Repository.Implementations
{
    public class PortalMemberRepository : RepositoryBase<PortalMember>, IPortalMemberRepository
    {
        public PortalMemberRepository(IDatabaseFactory databaseFactory) : base(databaseFactory)
        {
        }

        public async Task<long> CreatePortalMember(SignUpModel signUp)
        {
            BeginTransaction();
            var portalMember = new PortalMember()
            {
                MemberCode = "12345",
                OfficeID = signUp.OfficeID,
                CenterID = signUp.CenterID,
                GroupID = signUp.GroupID,
                JoinDate = signUp.JoinDate,
                Gender = signUp.Gender,
                MemberCategoryID = 1,
                MemberStatus = "AC",
                OrgID = signUp.OrgID,
                FirstName = signUp.FirstName,
                LastName = signUp.LastName,
                Email = signUp.Email,
                Occupation = signUp.Occupation,
                Address = signUp.Address,
                Photo = signUp.NidPic,
                Phone = signUp.PhoneNumber,
                HasRequestedApproval = false,
                MemberAge = signUp.MemberAge
            };
            DataContext.Add(portalMember);
            CommitTransaction();
            return portalMember.Id;

        }
    }
}
