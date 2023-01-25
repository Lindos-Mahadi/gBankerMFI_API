using GC.MFI.Models.DbModels;
using GC.MFI.Services.Modules.GcMfi.Interfaces;
using GC.MFI.Services.Modules.Security.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;

namespace GC.MFI.WebApi.Controllers.Modules.GcMfi
{
    [Route("api/gcmfi/PortalMemberNominee")]
    public class PortalMemberNomineeController : GCMcfinaLegacyBaseController<PortalMemberNominee>
    {
        private readonly ILogger<PortalMemberNomineeController> _logger;
        private readonly IPortalMemberNomineeService _service;

        public PortalMemberNomineeController(ILogger<PortalMemberNomineeController> logger, IPortalMemberNomineeService service) : base(service)
        {
            this._logger = logger;
            this._service = service;
        }

      
    }
}
