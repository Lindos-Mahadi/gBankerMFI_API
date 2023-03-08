using GC.MFI.Models.ViewModels;
using GC.MFI.Services.Modules.GcMfi.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace GC.MFI.WebApi.Controllers.Modules.GcMfi
{
    [Route("api/ghealth/")]
    [ApiController]
    public class GHealthController : ControllerBase
    {
        private readonly IGHealthSecurityService _healthSecurityService;
        public GHealthController(IGHealthSecurityService healthSecurityService)
        {
            this._healthSecurityService = healthSecurityService;
        }
        [HttpPost]
        [Route("security")]
        public async Task<IActionResult> Authentication([FromQuery]string mobile)
        {
            var verified = await _healthSecurityService.IsGHealthLoggedIn(mobile);
            return Ok(verified);
        }
    }
}
