using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GC.MFI.Models.Models
{
    public class LoanLedger
    {
        public int SLNo { get; set; }
        public int LoanTrxID { get; set; }
        public int MemberID { get; set; }
        public decimal TRLoanPaid { get; set; }
        public decimal TRIntPaid { get; set; }
        public decimal OnlyLoanInstt { get; set; }
        public decimal OnlyInterestPaid { get; set; }
        public string OfficeCode { get; set; }
        public string OfficeName { get; set; }
        public string CenterCode { get; set; }
        public string CenterName { get; set; }
        public string MemberCode { get; set; }
        public string MemberName { get; set; }
        public string ProductCode { get; set; }
        public string ProductName { get; set; }
        public int LoanTerm { get; set; }
        public decimal PrincipalLoan { get; set; }
        public decimal TotalLoanPaid { get; set; }
        public decimal LoanBalance { get; set; }
        public decimal LoanDue { get; set; }
        public decimal IntCharge { get; set; }
        public decimal TotalInterestPaid { get; set; }
        public decimal IntDue { get; set; }
        public decimal InstallmentDate { get; set; }
        public string EmpName { get; set; }
        public decimal RunningLoanBalance { get; set; }
        public decimal RunningInterestBalance { get; set; }
    }
}
