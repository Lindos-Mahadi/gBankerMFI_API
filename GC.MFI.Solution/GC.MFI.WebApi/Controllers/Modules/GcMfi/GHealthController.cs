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
        public async Task<IActionResult> Authenticate(GHealthSignInViewModel model)
        {
            var header = AuthenticationHeaderValue.Parse(Request.Headers["Authorization"]).Parameter;
            var tokenDecode = JwtTokenDecode.GetDetailsFromToken(header);
            string username = tokenDecode.UserEmail;
            var verified = await _healthSecurityService.Authenticate(username,model.Password);
            if (verified != null)
                return Ok(verified);
            return Unauthorized();
        }
        [HttpPost]
        [Route("signup")]
        public async Task<IActionResult> SignUp(GHealthSignUpViewModel entity)
        {
            var header = AuthenticationHeaderValue.Parse(Request.Headers["Authorization"]).Parameter;
            var tokenDecode = JwtTokenDecode.GetDetailsFromToken(header);
            entity.MemberId = long.Parse(tokenDecode.MemberID);
            var registraton = await _healthSecurityService.SignUp(entity);
            if(registraton != null)
            {
                return Ok(registraton);
            }
            object msg = "Data Alredy Exists";
            return Ok(msg);
        }
    }
}
