using GC.MFI.DataAccess;
using GC.MFI.Models.DbModels;
using GC.MFI.Models.ViewModels;
using GC.MFI.Services.Modules.BntPos.Interfaces;
using GC.MFI.Services.Modules.GcMfi.Interfaces;
using GC.MFI.WebApi.Controllers.Modules.Pos;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System.Text.Json.Nodes;
using static System.Net.Mime.MediaTypeNames;

namespace GC.MFI.WebApi.Controllers.Modules.GcMfi
{
    [Route("api/gcmfi/NotificationTable")]
    public class NotificationTableController : GcMfiMembePortalBaseController<NotificationTableViewModel, NotificationTable>
    {
        private readonly ILogger<NotificationTableController> _logger;
        private readonly INotificationTableService _service;
        private readonly GBankerDbContext dbContext;
        public NotificationTableController(ILogger<NotificationTableController> logger, GBankerDbContext dbContext, INotificationTableService service) : base(service)
        {
            _logger = logger;
            _service = service;
            this.dbContext = dbContext;
        }


        //[HttpPost("create")]
        //public override NotificationTableViewModel Create(NotificationTableViewModel objectToSave)
        //{
        //    var create = _service.Create(objectToSave);
        //    var modifiedEntities = dbContext.ChangeTracker.Entries()
        //                        .Where(e => e.State == EntityState.Modified)
        //                        .ToList();

        //    Get the list of added entities
        //    var addedEntities = dbContext.ChangeTracker.Entries()
        //                                .Where(e => e.State == EntityState.Added)
        //                                .ToList();

        //    Get the list of deleted entities
        //    var deletedEntities = dbContext.ChangeTracker.Entries()
        //                                .Where(e => e.State == EntityState.Deleted)
        //                                .ToList();

        //    Do something with the modified, added, and deleted entities
        //     such as saving the changes to the database
        //    dbContext.SaveChanges();
        //    return create;
        //}
    }
}

