using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
  
namespace GC.MFI.Models.Models
{
    public partial class RepaymentScheduleReportF
    {
        public int OfficeID { get; set; }
        public string OfficeCode { get; set; }
        public string OfficeName { get; set; }
        public int CenterID { get; set; }
        public string CenterCode { get; set; }
        public string CenterName { get; set; }
        public short ProductID { get; set; }
        public string ProductCode { get; set; }
        public string ProductName { get; set; }
        public int Duration { get; set; }
        public long MemberID { get; set; }
        public string MemberCode { get; set; }
        public string MemberName { get; set; }
        public string DisburseDate { get; set; }
        public string RepaymentDate { get; set; }
        public int InstallmentNo { get; set; }
        public decimal TotalInstallment { get; set; }
        public decimal PrincipalLoan { get; set; }
        public Decimal? LoanBalnce { get; set; }
        public Decimal? IntCharge { get; set; }
        public Decimal? IntPAid { get; set; }
        public Int32? InterestBalance { get; set; }
        public int LoanTerm { get; set; }
        public string Name { get; set; }
    }
}
