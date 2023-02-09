using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GC.MFI.Models.DbModels
{
    public partial class PortalMember : DbModelBase, IDbModelBase
    {
        public string MemberCode { get; set; }
        public int OfficeID { get; set; }
        public int CenterID { get; set; }
        public Int16 GroupID { get; set; }
        public DateTime? JoinDate { get; set; }
        public string Gender { get; set; }
        public Byte MemberCategoryID { get; set; }
        public string MemberStatus { get; set; }
        public int OrgID { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string FatherName { get; set; }
        public string MotherName { get; set; }
        public string Email { get; set; }
        public string Occupation { get; set; }
        public string Address { get; set; }
        public string Photo { get; set; }
        public string Phone { get; set; }
        public bool? ApprovalStatus { get; set; }
        public int MemberAge { get; set; }
        public string EducationQualification { get; set; }
        public string DistrictCode { get; set; }
        public string DivisionCode { get; set; }
        public string UnionCode { get; set; }
        public string VillageCode { get; set; }
        public string UpozillaCode { get; set; }
        public int CountryID { get; set; }
        public DateTime DOB { get; set; }
        public string PostCode { get; set; }
        public long MemberNID { get; set; }
        public long Image { get; set; }
        public string NationalID {get;set;}
        public string PlaceOfBirth { get;set;}
        public string Cityzenship { get;set;}
        public string MaritalStatus { get;set;}
        public string HomeType { get; set; }
        public string SpouseName { get; set; }
        public string SpouseNameBN { get; set; }
    }
}
