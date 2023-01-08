using System;

namespace GC.MFI.Models.DbModels
{
    public interface IViewModelBase
    {     
        Guid Id { get; set; }
        string CreateUser { get; set; }
        DateTime? CreateDate { get; set; }
        string UpdateUser { get; set; }
        DateTime? UpdateDate { get; set; }
        string Status { get; set; }
        string StatusDesc { get; }
    }
}
