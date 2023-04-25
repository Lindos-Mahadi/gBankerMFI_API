using GC.MFI.Models;
using GC.MFI.Models.DbModels;
using GC.MFI.Models.ViewModels;
using GC.MFI.Services.Modules.GcMfi.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace GC.MFI.WebApi.Controllers.Modules.GcMfi
{
    [Route("api/gcmfi/SingalRConnectionTable")]
    public class SignalRConnectionTableController : GCMcfinaLegacyBaseController<SignalRConnectionTable>
    {
        private readonly ILogger<SignalRConnectionTableController> _logger;
        private readonly ISignalRConnectionTableService _service;

        public SignalRConnectionTableController(ILogger<SignalRConnectionTableController> logger, ISignalRConnectionTableService service) : base(service)
        {
            _logger = logger;
            _service = service;
        }

        [HttpPost]
        [Route("create")]
        public SignalRConnectionTable Create(SignalRConnectionTable singalRConnectionTable) 
        {
            _service.Create(singalRConnectionTable);
            return singalRConnectionTable;
        }
    }
}
