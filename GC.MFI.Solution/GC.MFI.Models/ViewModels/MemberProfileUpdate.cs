using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace GC.MFI.Models.ViewModels
{
    public class MemberProfileUpdate
    {
        public long memberId { get; set; }
        public string BanglaName { get; set; }
        public string FatherNameBN { get; set; }
        public string MotherNameBN { get; set; }
        public string SpouseName { get; set; }
        public string SpouseNameBN { get; set; }

        public int? IdentTypeID { get; set; }
        public DateTime? CardIssueDate { get; set; }
        public DateTime? ExpireDate { get; set; }
        public string OtherIdNo { get; set; }
        public int? ProvidedByCountryID { get; set; }

        public int? FamilyMember { get; set; }
        public string FamilyContactNo { get; set; }
        public string RefereeName { get; set; }
        public string CoApplicantName { get; set; }
        public string TotalWealth { get; set; }
        public string MemCategory { get; set; }
        public string TIN { get; set; }
        public decimal? TaxAmount { get; set; }

        public bool? IsAnyFS { get; set; }
        public string FServiceName { get; set; }
        public int? FinServiceChoiceId { get; set; }
        public int? TransactionChoiceId { get; set; }
        //public string UpdateUser { get; set; }
    }
}
