namespace GC.MFI.Models.DbModels
{
    public class LoanAccReschedule : DbModelBase, IDbModelBase
    {
        public long MemberID { get; set; }
        public long OfficeID { get; set; }
        public long LoanID { get; set; }
    }
}