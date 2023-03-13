using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GC.MFI.Models.DbModels.BaseModels;

namespace GC.MFI.Models.DbModels
{
    public class LoanSummary :  LegacyDbModelBase, ILegacyDbModelBase
    {
        [Key]
        public long LoanSummaryID { get; set; }

        public int OfficeID { get; set; }

        public long MemberID { get; set; }

        public short ProductID { get; set; }

        public int CenterID { get; set; }

        public byte MemberCategoryID { get; set; }

        public byte LoanTerm { get; set; }

        public short PurposeID { get; set; }

        [StringLength(100)]
        public string LoanNo { get; set; }

        [Column(TypeName = "numeric")]
        public decimal PrincipalLoan { get; set; }

        [Column(TypeName = "date")]
        public DateTime ApproveDate { get; set; }

        [Column(TypeName = "date")]
        public DateTime? DisburseDate { get; set; }

        public int Duration { get; set; }

        [Column(TypeName = "numeric")]
        public decimal LoanRepaid { get; set; }

        [Column(TypeName = "numeric")]
        public decimal IntCharge { get; set; }

        [Column(TypeName = "numeric")]
        public decimal IntPaid { get; set; }

        [Column(TypeName = "numeric")]
        public decimal LoanInstallment { get; set; }

        [Column(TypeName = "numeric")]
        public decimal IntInstallment { get; set; }

        [Column(TypeName = "numeric")]
        public decimal InterestRate { get; set; }

        [Column(TypeName = "smalldatetime")]
        public DateTime? InstallmentStartDate { get; set; }

        public int InstallmentNo { get; set; }

        public int DropInstallment { get; set; }

        public int Holidays { get; set; }

        [Column(TypeName = "date")]
        public DateTime? InstallmentDate { get; set; }

        public byte TransType { get; set; }

        public short ContinuousDrop { get; set; }

        public byte LoanStatus { get; set; }

        [Column(TypeName = "numeric")]
        public decimal Balance { get; set; }

        [Column(TypeName = "numeric")]
        public decimal Advance { get; set; }

        [Column(TypeName = "numeric")]
        public decimal DueRecovery { get; set; }

        [Column(TypeName = "date")]
        public DateTime? LoanCloseDate { get; set; }

        [Column(TypeName = "date")]
        public DateTime? OverdueDate { get; set; }

        public short EmployeeId { get; set; }

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

        public byte InvestorID { get; set; }

        public bool Posted { get; set; }

        public bool? IsActive { get; set; }

        [Column(TypeName = "smalldatetime")]
        public DateTime? InActiveDate { get; set; }

        [Required]
        [StringLength(35)]
        public string CreateUser { get; set; }

        [Column(TypeName = "smalldatetime")]
        public DateTime CreateDate { get; set; }
        public int OrgID { get; set; }
        [StringLength(50)]
        public string BankName { get; set; }

        public string Remarks { get; set; }

        [StringLength(50)]
        public string ChequeNo { get; set; }

        public bool? IsApproved { get; set; }


        public string LoanAccountNo { get; set; }
        [StringLength(100)]
        public string CoApplicantName { get; set; }

        [StringLength(50)]
        public string Guarantor { get; set; }

        public long? MemberPassBookRegisterID { get; set; }
        public string SecurityBankName { get; set; }

        public string SecurityBankBranchName { get; set; }

        public string SecurityBankCheckNo { get; set; }
        public DateTime? ChequeIssueDate { get; set; }
        [Column(TypeName = "smalldatetime")]
        public DateTime? FirstInstallmentStartDate { get; set; }
       
      

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
        public long? PortalLoanSummaryID { get; set; }
        public long? GuarantorNID { get; set; }
        public long? GuarantorImg { get; set; }
        public string SupportingDocumentsId { get; set; }
    }
}
