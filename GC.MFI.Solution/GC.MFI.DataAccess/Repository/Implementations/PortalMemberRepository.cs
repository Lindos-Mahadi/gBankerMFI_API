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

            var year = signUp.DOB.AddYears(-DateTime.Now.Year).Year;
            BeginTransaction();
            var portalMember = new PortalMember()
            {
                MemberCode = "12345",
                OfficeID = signUp.OfficeID,
                CenterID = signUp.CenterID,
                GroupID = signUp.GroupID,
                JoinDate = DateTime.Now,
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
               EducationQualification = signUp.EducationQualification,
                ApprovalStatus = false,
                DOB= signUp.DOB,
                MemberAge = year,
                District = signUp.District,
                Division= signUp.Division,
                Upazilla = signUp.Upazilla,
                PostCode = signUp.PostCode,
                CreateDate= DateTime.Now,
                UpdateDate= DateTime.Now,
                Status = "A"


            };
            DataContext.Add(portalMember);
            CommitTransaction();
            return portalMember.Id;

        }
    }
}
