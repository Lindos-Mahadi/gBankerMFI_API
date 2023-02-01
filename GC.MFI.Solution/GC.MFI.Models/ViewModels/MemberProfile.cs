using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GC.MFI.Models.ViewModels
{
    public class MemberProfile
    {
        public long MemberId { get; set; }
        public string Gender { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string FatherName { get; set; }
        public string MotherName { get; set; }
        public string Email { get; set; }
        public string Occupation { get; set; }
        public string Address { get; set; }
        public string Photo { get; set; }
        public string Phone { get; set; }
        public int MemberAge { get; set; }
        public string EducationQualification { get; set; }
        public string DistrictCode { get; set; }
        public string DivisionCode { get; set; }
        public string UpozillaCode { get; set; }
        public int countryID { get; set; }
        public DateTime DOB { get; set; }
        public string postCode { get; set; }
    }
}
