using Microsoft.AspNetCore.Identity;
using Microsoft.VisualBasic;
using System;
using System.ComponentModel.DataAnnotations;
using System.Globalization;

namespace GC.MFI.Models.Modules.Distributions.Security
{
    public class ApplicationUser : IdentityUser 
    {
        //public ApplicationUser()
        //{
        //    DateCreated = DateTime.Now;
        //}
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string ProfilePicUrl { get; set; }
        public DateTime DateCreated { get; set; }
        public DateTime? LastLoginTime { get; set; }
        public bool Activated { get; set; }
        public int RoleId { get; set; }
        //public bool? IsTemporaryPassword { get; set; }
        public Int16? EmployeeID { get; set; }
        public long? PortalMemberID { get; set; }
        //public string DisplayName
        //{
        //    get { return FirstName + " " + LastName; }
        //}
        // public virtual Employee Employee { get; set; }
    }
}
