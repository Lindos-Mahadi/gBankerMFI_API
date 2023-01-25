using GC.MFI.Models.DbModels;
using GC.MFI.Services.Modules.GcMfi.Interfaces;
using GC.MFI.Services.Modules.Security.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;

namespace GC.MFI.WebApi.Controllers.Modules.GcMfi
{
    [Route("api/gcmfi/PortalMemberNominee")]
    public class NomineeXPortalSavingSummaryController : GCMcfinaLegacyBaseController<NomineeXPortalSavingSummary>
    {
        private readonly ILogger<NomineeXPortalSavingSummaryController> _logger;
        private readonly INomineeXPortalSavingSummaryService _service;

        public NomineeXPortalSavingSummaryController(ILogger<NomineeXPortalSavingSummaryController> logger, INomineeXPortalSavingSummaryService service) : base(service)
        {
            this._logger = logger;
            this._service = service;
        }

      
    }
}
