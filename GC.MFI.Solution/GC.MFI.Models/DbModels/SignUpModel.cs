using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GC.MFI.Models.DbModels
{
    public class SignUpModel
    {
        [Required]
        public string UserName { get; set; }
        public string Password { get; set; }
        public string Email { get; set; }
        public string FirstName { get; set; }

        public string LastName { get; set; }
        public string FatherName { get; set; }
        public string MotherName { get; set; }
        [Required]
        public string Occupation { get; set; }
        public string PhoneNumber { get; set; }

        [Required]
        public string Address { get; set; }
        public string NidPic { get; set; }
        public string MemberCode { get; set; }
        [Required]
        public int OfficeID { get; set; }
        public DateTime? JoinDate { get; set; }
        public Int16 GroupID { get; set; }
        public int CenterID { get; set; }
        [Required]
        public string Gender { get; set; }
        //[Required]
        //public int MemberCategoryID { get; set; }
        public string MemberStatus { get; set; }
        [Required]
        public int OrgID { get; set; }
        public DateTime DOB { get; set; }

        public string DistrictCode { get; set; }
        public string DivisionCode { get; set; }
        public string UpozillaCode { get; set; }
        public string PostCode { get; set; }
        public string UnionCode { get; set; }
        public string VillageCode { get; set; }
        public int CountryID { get; set; }
        public string EducationQualification { get; set; }
        public string Image { get; set; }

    }
}
