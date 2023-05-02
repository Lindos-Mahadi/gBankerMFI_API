using GC.MFI.Models.ViewModels;
using GC.MFI.Services.Modules.GcMfi.Interfaces;
using GC.MFI.Utility.Helpers;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Net.Http.Headers;

namespace GC.MFI.WebApi.Controllers.Modules.GcMfi
{
    [Authorize]
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
        public async Task<DashboardModel> GetDashboard()
        {
            var header = AuthenticationHeaderValue.Parse(Request.Headers["Authorization"]).Parameter;
            var tokeninfo = JwtTokenDecode.GetDetailsFromToken(header);
            var result = await _service.GetDashboardInfo(tokeninfo.MemberID);
            return result;
        }
    }
}
