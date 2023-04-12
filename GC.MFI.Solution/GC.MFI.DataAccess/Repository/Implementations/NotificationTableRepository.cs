using GC.MFI.DataAccess.InfrastructureBase;
using GC.MFI.DataAccess.Repository.Interfaces;
using GC.MFI.DataAccess.Repository.Interfaces.Security;
using GC.MFI.Models.DbModels;
using GC.MFI.Models.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GC.MFI.DataAccess.Repository.Implementations
{
    public class NotificationTableRepository : RepositoryBase<NotificationTable>, INotificationTableRepository
    {
        public NotificationTableRepository(IDatabaseFactory databaseFactory) : base(databaseFactory)
        {
        }


    }
}
