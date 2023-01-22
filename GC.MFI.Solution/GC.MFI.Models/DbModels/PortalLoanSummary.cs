using GC.MFI.Models.DbModels.BaseModels;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace GC.MFI.Models.DbModels
{
    [Table("PortalLoanSummary")]
    public partial class PortalLoanSummary : LegacyDbModelBase, ILegacyDbModelBase
    {
        [Key]
        public long PortalLoanSummaryID { get; set; }
        [Required]
        public int OfficeID { get; set; }
        [Required]
        public long MemberID { get; set; }
        [Required]
        public short ProductID { get; set; }
        [Required]
        public int CenterID { get; set; }
        [Required]
        public byte MemberCategoryID { get; set; }
        //[Required]
        public byte LoanTerm { get; set; }

        public short? PurposeID { get; set; }

        [StringLength(100)]
        public string LoanNo { get; set; }
        [Required]
        [Column(TypeName = "numeric")]
        public decimal PrincipalLoan { get; set; }
        [Required]
        [Column(TypeName = "date")]
        public DateTime ApproveDate { get; set; }

        [Column(TypeName = "date")]
        public DateTime? DisburseDate { get; set; }
        [Required]
        public int Duration { get; set; }
        [Required]
        [Column(TypeName = "numeric")]
        public decimal LoanRepaid { get; set; }
        [Required]
        [Column(TypeName = "numeric")]
        public decimal IntCharge { get; set; }
        [Required]
        [Column(TypeName = "numeric")]
        public decimal IntPaid { get; set; }
        [Required]
        [Column(TypeName = "numeric")]
        public decimal LoanInstallment { get; set; }
        [Required]
        [Column(TypeName = "numeric")]
        public decimal IntInstallment { get; set; }
        [Required]
        [Column(TypeName = "numeric")]
        public decimal InterestRate { get; set; }

        [Column(TypeName = "smalldatetime")]
        public DateTime? InstallmentStartDate { get; set; }
        [Required]
        public int InstallmentNo { get; set; }
        [Required]
        public int DropInstallment { get; set; }
        [Required]
        public int Holidays { get; set; }

        [Column(TypeName = "date")]
        public DateTime? InstallmentDate { get; set; }
        [Required]
        public byte TransType { get; set; }
        [Required]
        public short ContinuousDrop { get; set; }
        [Required]
        public byte LoanStatus { get; set; }
        [Required]
        [Column(TypeName = "numeric")]
        public decimal Balance { get; set; }
        [Required]
        [Column(TypeName = "numeric")]
        public decimal Advance { get; set; }
        [Required]
        [Column(TypeName = "numeric")]
        public decimal DueRecovery { get; set; }

        [Column(TypeName = "date")]
        public DateTime? LoanCloseDate { get; set; }

        [Column(TypeName = "date")]
        public DateTime? OverdueDate { get; set; }
        [Required]
        public short EmployeeId { get; set; }

        public byte? InvestorID { get; set; }
        [Required]
        [Column(TypeName = "numeric")]
        public decimal ExcessPay { get; set; }

        [Column(TypeName = "numeric")]
        public decimal? CurLoan { get; set; }

        [Column(TypeName = "numeric")]
        public decimal? PreLoan { get; set; }

        [Column(TypeName = "numeric")]
        public decimal? CumLoanDue { get; set; }

        [Column(TypeName = "numeric")]
        public decimal? WriteOffLoan { get; set; }

        [Column(TypeName = "numeric")]
        public decimal? WriteOffInterest { get; set; }
        [Required]
        public bool Posted { get; set; }
        [Required]
        public int OrgID { get; set; }

        public bool? IsActive { get; set; }

        [Column(TypeName = "smalldatetime")]
        public DateTime? InActiveDate { get; set; }

        [Required]
        [StringLength(35)]
        public string CreateUser { get; set; }
        [Required]
        [Column(TypeName = "smalldatetime")]
        public DateTime CreateDate { get; set; }

        [StringLength(50)]
        public string BankName { get; set; }

        [StringLength(50)]
        public string ChequeNo { get; set; }

        public bool? IsApproved { get; set; }

        [StringLength(50)]
        public string CoApplicantName { get; set; }

        [StringLength(50)]
        public string Guarantor { get; set; }

        public long? MemberPassBookRegisterID { get; set; }

        public DateTime? ChequeIssueDate { get; set; }

        [Column(TypeName = "numeric")]
        public decimal? CumIntDue { get; set; }

        [Column(TypeName = "numeric")]
        public decimal? ApprovedAmount { get; set; }

        [Column(TypeName = "numeric")]
        public decimal? PartialAmount { get; set; }

        public byte? FinalDisbursement { get; set; }

        public byte? DisbursementType { get; set; }

        [Column(TypeName = "numeric")]
        public decimal? PartialIntCharge { get; set; }

        [Column(TypeName = "numeric")]
        public decimal? PartialIntPaid { get; set; }

        [Column(TypeName = "date")]
        public DateTime? FirstInstallmentDate { get; set; }

        public DateTime? FirstInstallmentStartDate { get; set; }

        [Column(TypeName = "numeric")]
        public decimal? CurIntPaid { get; set; }

        [Column(TypeName = "numeric")]
        public decimal? CurIntCharge { get; set; }

        [StringLength(50)]
        public string LoanAccountNo { get; set; }

        [StringLength(70)]
        public string SecurityBankName { get; set; }

        [StringLength(70)]
        public string SecurityBankBranchName { get; set; }

        [StringLength(70)]
        public string SecurityBankCheckNo { get; set; }

        [Column(TypeName = "numeric")]
        public decimal? CurLoanDue { get; set; }

        [Column(TypeName = "numeric")]
        public decimal? CurIntDue { get; set; }

        public int? LastInstallmentNo { get; set; }

        [Column(TypeName = "numeric")]
        public decimal? CSFRate { get; set; }

        [Column(TypeName = "numeric")]
        public decimal? CSFAmount { get; set; }

        [StringLength(50)]
        public string Remarks { get; set; }
        public bool ApprovalStatus { get; set; } = false;
    }
}
