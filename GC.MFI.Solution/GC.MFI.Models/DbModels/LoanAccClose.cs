namespace GC.MFI.Models.DbModels
{
    public class LoanAccClose : DbModelBase, IDbModelBase
    {
        public long MemberID { get; set; }
        public long OfficeID { get; set; }
        public long LoanID { get; set; }
    }
}