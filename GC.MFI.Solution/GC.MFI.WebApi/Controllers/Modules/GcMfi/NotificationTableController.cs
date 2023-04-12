using GC.MFI.Models.DbModels;
using GC.MFI.Models.ViewModels;
using GC.MFI.Services.Modules.BntPos.Interfaces;
using GC.MFI.Services.Modules.GcMfi.Interfaces;
using GC.MFI.WebApi.Controllers.Modules.Pos;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
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
        public NotificationTableController(ILogger<NotificationTableController> logger, INotificationTableService service) : base(service)
        {
            _logger = logger;
            _service = service;
        }
    }
}

