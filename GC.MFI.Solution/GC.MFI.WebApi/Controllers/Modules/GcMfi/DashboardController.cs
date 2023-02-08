using GC.MFI.Models.ViewModels;
using GC.MFI.Services.Modules.GcMfi.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace GC.MFI.WebApi.Controllers.Modules.GcMfi
{
    [Route("api/gcmfi/dashboard")]
    [ApiController]
    public class DashboardController : ControllerBase
    {
        private readonly ILogger<DashboardController> _logger;
        private readonly IDashboardService _service;
        public DashboardController (ILogger<DashboardController> logger, IDashboardService service)
        {
            this._logger = logger;
            this._service = service;
        }
        [HttpGet]
        [Route("getdashboard")]
        public async Task<DashboardModel> GetDashboard(long MemberId)
        {
           var result = await _service.GetDashboardInfo(MemberId);
            return result;
        }
    }
}
