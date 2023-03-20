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
                               select new MemberProfile
                               {
                                   PortalMemberId = m.Id,
                                   Gender = m.Gender,
                                   FirstName = m.FirstName,
                                   LastName = m.LastName,
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
                               }).Where(t=>  t.PortalMemberId == Id ).FirstOrDefault();
            return portalMember;
        }


        public void CreatePortalMemberNIDandImage(long portalMemberId, long portalMemberFId, long portalMemberIId)
        {
            var memberId = DataContext.PortalMember.Find(portalMemberId);
            memberId.MemberNID= portalMemberFId;
            memberId.Image = portalMemberIId;
            DataContext.SaveChanges();
        }

        
    }
}
