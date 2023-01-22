using GC.MFI.Models.DbModels;
using GC.MFI.Services.Modules.GcMfi.Interfaces;
using GC.MFI.Services.Modules.Security.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;

namespace GC.MFI.WebApi.Controllers.Modules.GcMfi
{
    [Route("api/gcmfi/PortalSavingSummary")]
    public class PortalSavingSummaryController : GCMcfinaLegacyBaseController<PortalSavingSummary>
    {
        private readonly ILogger<PortalSavingSummaryController> _logger;
        private readonly IPortalSavingSummaryService _service;

        public PortalSavingSummaryController(ILogger<PortalSavingSummaryController> logger, IPortalSavingSummaryService service) : base(service)
        {
            this._logger = logger;
            this._service = service;
        }

        
    }
}
