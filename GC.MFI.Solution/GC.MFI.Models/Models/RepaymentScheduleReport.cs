using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GC.MFI.Models.Models
{
    public partial class RepaymentScheduleReportAE
    {
        public int OfficeID { get; set; }
        public string OfficeCode { get; set; }
        public string OfficeName { get; set; }
        public int CenterID { get; set; }
        public string CenterCode{ get; set; }
        public string CenterName{ get; set; }
        public int ProductID{ get; set; }
        public string ProductCode{ get; set; }
        public string ProductName{ get; set; }
        public int Duration{ get; set; }
        public long MemberID{ get; set; }
        public string MemberCode{ get; set; }
        public string MemberName{ get; set; }
        public DateTime? DisburseDate{ get; set; }
        public string RepaymentDate{ get; set; }
        public Decimal PrincipalLoan{ get; set; }
        //public Decimal LoanInstallMent{ get; set; }
        //public Decimal InterestInst{ get; set; }
        //public Decimal LoanPaid { get; set; }
        public Decimal IntPAid { get; set; }
        public Decimal LoanBalnce { get; set; }
        public int InstallmentNo{ get; set; }
        public Decimal TotalInstallment{ get; set; }
        public Decimal IntCharge { get; set; }
        public int LoanTerm{ get; set; }
        public string Name{ get; set; }
    }
}
