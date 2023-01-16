using GC.MFI.Models.DbModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using XenterSolution.Models.ViewModels;

namespace GC.MFI.Models.ViewModels
{
    public class PortalMemberViewModel : ViewModelBase, IViewModelBase
    {
        public string MemberCode { get; set; }
        public int OfficeID { get; set; }
        public int CenterID { get; set; }
        public Int16 GroupID { get; set; }
        public DateTime JoinDate { get; set; }
        public string Gender { get; set; }
        public Byte MemberCategoryID { get; set; }
        public string MemberStatus { get; set; }
        public int OrgID { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
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
        public string UpozillaCode { get; set; }
        public int CountryID { get; set; }
        public DateTime DOB { get; set; }
        public string PostCode { get; set; }
    }
}
