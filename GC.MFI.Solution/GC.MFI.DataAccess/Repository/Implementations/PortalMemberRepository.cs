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

        public async Task<PortalMember> CreatePortalMember(SignUpModel signUp)
        {

            int now = int.Parse(DateTime.Now.ToString("yyyyMMdd"));
            int dob = int.Parse(signUp.DOB.ToString("yyyyMMdd"));
            int age = (now - dob) / 10000;
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
                FatherName = signUp.FatherName,
                MotherName = signUp.MotherName,
                Email = signUp.Email,
                Occupation = signUp.Occupation,
                Address = signUp.Address,
                Photo = "",
                Phone = signUp.PhoneNumber,
               EducationQualification = signUp.EducationQualification,
                ApprovalStatus = false,
                DOB= signUp.DOB,
                MemberAge = age,
                CountryID = signUp.CountryID,
                DistrictCode = signUp.DistrictCode,
                DivisionCode= signUp.DivisionCode,
                UpozillaCode = signUp.UpozillaCode,
                PostCode = signUp.PostCode,
                UnionCode = signUp.UnionCode,
                VillageCode = signUp.VillageCode,
                CreateDate= DateTime.Now,
                UpdateDate= DateTime.Now,
                Status = "A",
                NationalID = signUp.NationalID,
                PlaceOfBirth = signUp.PlaceOfBirth,
                Cityzenship = signUp.Cityzenship,
                MaritalStatus = signUp.MaritalStatus,
                HomeType = signUp.HomeType,
                SpouseName = signUp.SpouseName,
                SpouseNameBN = signUp.SpouseNameBN


            };
            DataContext.Add(portalMember);
            DataContext.SaveChanges();
            return portalMember;
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
        public void CreatePortalMemberNID(long portalMemberId, long portalMemberFId)
        {
            BeginTransaction();
            var memberId = DataContext.PortalMember.Where(t=> t.Id == portalMemberId).FirstOrDefault();
            memberId.MemberNID= portalMemberFId;
            CommitTransaction();
        }

        public void CreatePortalMemberImage(long portalMemberId, long portalMemberIId)
        {
            BeginTransaction();
            var memberId = DataContext.PortalMember.Where(t => t.Id == portalMemberId).FirstOrDefault();
            memberId.Image = portalMemberIId;
            DataContext.SaveChanges();
            CommitTransaction();
        }
    }
}
