using GC.MFI.Models.DbModels;
using GC.MFI.Services.Modules.GcMfi.Interfaces;
using GC.MFI.Services.Modules.Security.Interfaces;
using Microsoft.AspNetCore.Components;

namespace GC.MFI.WebApi.Controllers.Modules.GcMfi
{
    [Route("api/gcmfi/upozilla")]
    public class UpozillaController : GCMcfinaLegacyBaseController<Upozilla>
    {
        private readonly ILogger<UpozillaController> _logger;
        private readonly IUpozillaService _service;

        public UpozillaController(ILogger<UpozillaController> logger, IUpozillaService service) : base(service)
        {
            this._logger = logger;
            this._service = service;
        }
    }
}
