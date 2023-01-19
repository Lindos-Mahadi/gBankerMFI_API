using GC.MFI.DataAccess.InfrastructureBase;
using GC.MFI.DataAccess.Repository.Interfaces;
using GC.MFI.Models.DbModels;
using GC.MFI.Models.ViewModels;
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

        public async Task<Member> UpdateMember(Member member)
        {
            var upM =  DataContext.Member.Find(member.MemberID);
            if (upM != null)
            {
                upM.MemberCode= member.MemberCode;
                upM.OldMemberCode= member.OldMemberCode;
                upM.BanglaName= member.BanglaName;
                upM.FirstName= member.FirstName;
                upM.MiddleName = member.MiddleName;
                upM.LastName= member.LastName;
                upM.AddressLine1= member.AddressLine1;
                upM.AddressLine2= member.AddressLine2;
                upM.CountryID = member.CountryID;
                upM.ProvidedByCountryID = member.ProvidedByCountryID;
                upM.FinServiceChoiceId = member.FinServiceChoiceId;
                upM.TransactionChoiceId = member.TransactionChoiceId;
                upM.DivisionCode = member.DivisionCode;
                upM.DistrictCode = member.DistrictCode;
                upM.UpozillaCode = member.UpozillaCode;
                upM.UnionCode = member.UnionCode;
                upM.VillageCode = member.VillageCode;
                upM.PerAddressLine1 = member.PerAddressLine1;
                upM.PerAddressLine2 = member.PerAddressLine2;
                upM.PerCountryID = member.PerCountryID;
                upM.PerDivisionCode = member.PerDivisionCode;
                upM.PerUpozillaCode = member.PerUpozillaCode;
                upM.PerUnionCode = member.PerUnionCode;
                upM.PerVillageCode = member.PerVillageCode;
                upM.PerZipCode = member.PerZipCode;
                upM.RefereeName = member.RefereeName;
                upM.BirthDate = member.BirthDate;
                upM.PlaceOfBirth = member.PlaceOfBirth;
                upM.Cityzenship = member.Cityzenship;
                upM.JoinDate = member.JoinDate;
                upM.RefereeName = member.RefereeName;
                upM.ExpireDate = member.ExpireDate;
                upM.Gender = member.Gender;
                upM.SmartCard = member.SmartCard;
                upM.NationalID = member.NationalID;
                upM.OtherIdNo = member.OtherIdNo;
                upM.IdentTypeID = member.IdentTypeID;
                upM.Location = member.Location;
                upM.HomeType = member.HomeType;
                upM.GroupType = member.GroupType;
                upM.Education = member.Education;
                upM.FamilyMember = member.FamilyMember;
                upM.TotalWealth = member.TotalWealth;
                upM.EconomicActivity = member.EconomicActivity;
                upM.FatherName = member.FatherName;
                upM.FatherNameBN = member.FatherNameBN;
                upM.SpouseName = member.SpouseName;
                upM.SpouseNameBN = member.SpouseNameBN;
                upM.TIN = member.TIN;
                upM.TaxAmount = member.TaxAmount;
                upM.MotherName = member.MotherName;
                upM.MotherNameBN = member.MotherNameBN;
                upM.CoApplicantName = member.CoApplicantName;
                upM.MemberCategoryID = member.MemberCategoryID;
                upM.MemberStatus = member.MemberStatus;
                upM.ReleaseDate = member.ReleaseDate;
                upM.City = member.City;
                upM.StateName = member.StateName;
                upM.ZipCode = member.ZipCode;
                upM.CountryOfIssue = member.CountryOfIssue;
                upM.NIDComments = member.NIDComments;
                upM.IDType = member.IDType;
                upM.Race = member.Race;
                upM.Ethnicity = member.Ethnicity;
                upM.Email = member.Email;
                upM.PhoneNo = member.PhoneNo;
                upM.nsAccountNo = member.nsAccountNo;
                upM.MemberType = member.MemberType;
                upM.MemberImg = member.MemberImg;
                upM.ThumbImg = member.ThumbImg;
                upM.PwdStatus = member.PwdStatus;
                upM.MemCategory = member.MemCategory;
                upM.MaritalStatus = member.MaritalStatus;
                upM.FServiceName = member.FServiceName;
                upM.IsActive = member.IsActive;
                upM.IsAnyFS = member.IsAnyFS;
                upM.InActiveDate = member.InActiveDate;
                upM.CreateUser = member.CreateUser;
                upM.CreateDate = member.CreateDate;
                upM.MemberNameBng = member.MemberNameBng;
                upM.AsOnDateAge = member.AsOnDateAge;
                upM.FamilyContactNo = member.FamilyContactNo;
                upM.CardIssueDate = member.CardIssueDate;
            }
            DataContext.SaveChanges();

            return upM; ;
        }
    }
}
