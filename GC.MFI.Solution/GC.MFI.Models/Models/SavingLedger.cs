using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GC.MFI.Models.Models
{
    public class SavingLedger
    {
        public int SLNo { get; set; }
        public int SavingTrxID { get; set; }
        public decimal TRWithdrawal { get; set; }
        public decimal TRPersonalSavings { get; set; }
        public string OfficeCode { get; set; }
        public string OfficeName { get; set; }
        public string CenterCode { get; set; }
        public string CenterName { get; set; }
        public int MemberID { get; set; }
        public string MemberCode { get; set; }
        public string MemberName { get; set; }
        public string ProductCode { get; set; }
        public string ProductName { get; set; }
        public int NoOfAccount { get; set; }
        public DateTime TransactionDate { get; set; }
        public decimal Penalty { get; set; }
        public decimal Deposit { get; set; }
        public decimal MonthlyInterest { get; set; }
        public decimal Withdrawal { get; set; }
        public decimal Balance { get; set; }
        public string EmpName { get; set; }
        public decimal RunningBalance { get; set; }

    }
}
