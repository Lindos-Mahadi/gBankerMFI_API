using GC.MFI.Models.DbModels;
using GC.MFI.Services.Modules.Firebase.Interfaces;
using GC.MFI.Services.Modules.GcMfi.Interfaces;
using GC.MFI.Services.Modules.Security.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;

namespace GC.MFI.WebApi.Controllers.Modules.GcMfi
{
    [Route("api/gcmfi/FcmConnectionTable")]
    public class FcmConnectionTableController : GCMcfinaLegacyBaseController<FcmConnectionTable>
    {
        private readonly ILogger<FcmConnectionTableController> _logger;
        private readonly IFcmConnectionTableService _service;

        public FcmConnectionTableController(ILogger<FcmConnectionTableController> logger, IFcmConnectionTableService service) : base(service)
        {
            this._logger = logger;
            this._service = service;
        }

        [HttpPost]
        [Route("create")]
        public FcmConnectionTable Create(FcmConnectionTable table)
        {
            var create = _service.Create(table);
            return create;
        }
        
        
        
    }
}
