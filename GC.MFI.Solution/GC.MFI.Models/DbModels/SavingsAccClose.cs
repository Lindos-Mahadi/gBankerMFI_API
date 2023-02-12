namespace GC.MFI.Models.DbModels
{
    public class SavingsAccClose : DbModelBase, IDbModelBase
    {
        public long MemberID { get; set; }
        public long OfficeID { get; set; }
        public long SavingAccountID { get; set; }
    }
}
