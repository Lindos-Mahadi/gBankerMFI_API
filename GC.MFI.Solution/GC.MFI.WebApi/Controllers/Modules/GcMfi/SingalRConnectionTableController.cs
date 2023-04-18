using GC.MFI.Models;
using GC.MFI.Models.DbModels;
using GC.MFI.Models.ViewModels;
using GC.MFI.Services.Modules.GcMfi.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace GC.MFI.WebApi.Controllers.Modules.GcMfi
{
    [Route("api/gcmfi/SingalRConnectionTable")]
    public class SingalRConnectionTableController : GCMcfinaLegacyBaseController<SingalRConnectionTable>
    {
        private readonly ILogger<SingalRConnectionTableController> _logger;
        private readonly ISingalRConnectionTableService _service;

        public SingalRConnectionTableController(ILogger<SingalRConnectionTableController> logger, ISingalRConnectionTableService service) : base(service)
        {
            _logger = logger;
            _service = service;
        }

        [HttpPost]
        [Route("create")]
        public SingalRConnectionTable Create(SingalRConnectionTable singalRConnectionTable) 
        {
            _service.Create(singalRConnectionTable);
            return singalRConnectionTable;
        }
    }
}
