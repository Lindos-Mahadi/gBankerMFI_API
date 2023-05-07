using GC.MFI.DataAccess;
using GC.MFI.Models.DbModels;
using GC.MFI.Models.ViewModels;
using GC.MFI.Services.Modules.BntPos.Interfaces;
using GC.MFI.Services.Modules.GcMfi.Interfaces;
using GC.MFI.Utility.Helpers;
using GC.MFI.WebApi.Controllers.Modules.Pos;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System.Net.Http.Headers;
using System.Text.Json.Nodes;
using static System.Net.Mime.MediaTypeNames;

namespace GC.MFI.WebApi.Controllers.Modules.GcMfi
{
    [Authorize]
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

        [HttpGet("ViewNotification")]
        public async Task<IActionResult?> ViewStatus(bool push)
        {
            if (push == false) return null;
            var header = AuthenticationHeaderValue.Parse(Request.Headers["Authorization"]).Parameter;
            var tokenifo = JwtTokenDecode.GetDetailsFromToken(header);
            await _service.ViewStatus(long.Parse(tokenifo.MemberID));
            return Ok();
        }



    }
}

