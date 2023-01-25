using GC.MFI.Models.DbModels;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GC.MFI.Models.ViewModels
{
    public class SavingSummaryViewModel
    {
        public long PortalSavingSummaryID { get; set; }
  
        public int OfficeID { get; set; }
  
        public long MemberID { get; set; }
        public string MemberName { get; set; }
    
        public short ProductID { get; set; }
        public string ProductName { get; set; }
        public int? CenterID { get; set; }

        public int? NoOfAccount { get; set; }

        [Column(TypeName = "date")]
        public DateTime? TransactionDate { get; set; }

        [Column(TypeName = "numeric")]
        public decimal? Deposit { get; set; }

        [Column(TypeName = "numeric")]
        public decimal? Withdrawal { get; set; }

        [Column(TypeName = "numeric")]
        public decimal? Balance { get; set; }

        [Column(TypeName = "numeric")]
        public decimal? InterestRate { get; set; }

        [Required]
        [Column(TypeName = "numeric")]
        public decimal SavingInstallment { get; set; }

        [Column(TypeName = "numeric")]
        public decimal? CumInterest { get; set; }

        [Column(TypeName = "numeric")]
        public decimal? MonthlyInterest { get; set; }

        [Column(TypeName = "numeric")]
        public decimal? Penalty { get; set; }

        [Column(TypeName = "date")]
        public DateTime OpeningDate { get; set; }

        [Column(TypeName = "date")]
        public DateTime? MaturedDate { get; set; }

        [Column(TypeName = "date")]
        public DateTime? ClosingDate { get; set; }

        public byte? TransType { get; set; }

        public byte? SavingStatus { get; set; }

        public short? EmployeeId { get; set; }

        public byte? MemberCategoryID { get; set; }

        public bool? Posted { get; set; }

        public bool? IsActive { get; set; }

        [Column(TypeName = "smalldatetime")]
        public DateTime? InActiveDate { get; set; }
        public int? Duration { get; set; }
        public int? InstallmentNo { get; set; }
        [Required]
        [StringLength(35)]
        public string CreateUser { get; set; }

        [Column(TypeName = "smalldatetime")]
        public DateTime CreateDate { get; set; }
        public int? OrgID { get; set; }

        public string SavingAccountNo { get; set; }
        public int? Ref_EmployeeID { get; set; }
        public bool? ApprovalStatus { get; set; }

        public virtual List<NomineeXPortalSavingSummary> MemberNomines { get; set; }
    }
}
