using System;

namespace GC.MFI.Models.DbModels
{
    public class DbModelBase : IDbModelBase
    {
        public long Id { get; set; }
        public string CreateUser { get; set; } = string.Empty;
        public DateTime? CreateDate { get; set; } = DateTime.Now;
        public string UpdateUser { get; set; } = string.Empty;
        public DateTime? UpdateDate { get; set; }
        public string Status { get; set; } = "A";
        public string StatusDesc { get { return Status == "A" ? "Active" : "Inactive"; } }
    }
}
