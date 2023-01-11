using GC.MFI.Models.DbModels;
using System;

namespace XenterSolution.Models.ViewModels
{
    public class ViewModelBase:IViewModelBase
    {       
        public long Id { get; set; }     
        public string CreateUser { get; set; } = string.Empty;
        public DateTime? CreateDate { get; set; }
        public string UpdateUser { get; set; } = string.Empty;
        public DateTime? UpdateDate { get; set; }
        public string Status { get; set; } = string.Empty;
        public string StatusDesc { get { return Status == "A" ? "Active" : "Inactive"; } }
    }
}
