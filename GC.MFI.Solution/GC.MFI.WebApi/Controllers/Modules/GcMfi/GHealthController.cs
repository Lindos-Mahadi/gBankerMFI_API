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
        [Route("ghealth_patient_details")]
        public async Task<IActionResult> Authentication([FromQuery]string mobile)
        {
            var verified = await _healthSecurityService.IsGHealthLoggedIn(mobile);
            if(verified != null)
                return Ok(verified);
            return NotFound();
        }
        [HttpPost]
        [Route("authenticate")]
        public async Task<IActionResult> Authenticate(string username, string password)
        {
            var verified = await _healthSecurityService.Authenticate(username,password);
            if (verified != null)
                return Ok(verified);
            return Unauthorized();
        }
    }
}
