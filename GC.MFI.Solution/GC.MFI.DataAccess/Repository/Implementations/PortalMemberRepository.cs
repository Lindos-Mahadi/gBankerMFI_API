using GC.MFI.DataAccess.InfrastructureBase;
using GC.MFI.DataAccess.Repository.Interfaces;
using GC.MFI.DataAccess.Repository.Interfaces.Security;
using GC.MFI.Models.DbModels;
using GC.MFI.Models.ViewModels;
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

        public async Task<MemberProfile> GetMemberById(long Id)
        {
            var portalMember =(from m in DataContext.PortalMember
                              where m.Id == Id
                               select new MemberProfile
                               {
                                   PortalMemberId = m.Id,
                                   Gender = m.Gender,
                                   FirstName = m.FirstName,
                                   LastName = m.LastName,
                                   FatherName = m.FatherName,
                                   MotherName = m.MotherName,
                                   Email = m.Email,
                                   Occupation = m.Occupation,
                                   Address = m.Address,
                                   Photo = m.Photo,
                                   Phone = m.Phone,
                                   MemberAge = m.MemberAge,
                                   EducationQualification = m.EducationQualification,
                                   DistrictCode = m.DistrictCode,
                                   DivisionCode = m.DivisionCode,
                                   UpozillaCode = m.UpozillaCode,
                                   countryID = m.CountryID,
                                   DOB = m.DOB,
                                   postCode = m.PostCode
                               }).FirstOrDefault();
            return portalMember;
        }


        public void CreatePortalMemberNIDandImage(long portalMemberId, long portalMemberFId, long portalMemberIId)
        {
            var memberId = GetById(portalMemberId);
            memberId.MemberNID= portalMemberFId;
            memberId.Image = portalMemberIId;
            DataContext.SaveChanges();
        }

        
    }
}
