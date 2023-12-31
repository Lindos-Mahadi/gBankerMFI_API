﻿using GC.MFI.Models.DbModels;
using GC.MFI.Models.ViewModels;
using System.Threading.Tasks;

namespace GC.MFI.Services.Modules.GcMfi.Interfaces
{
    public interface INotificationTableService : IServiceBase<NotificationTableViewModel, Models.DbModels.NotificationTable>
    {
        Task ViewStatus(long MemberId);
    }
}
