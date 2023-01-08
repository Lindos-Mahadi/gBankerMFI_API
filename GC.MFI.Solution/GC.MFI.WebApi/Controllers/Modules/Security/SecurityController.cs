using GC.MFI.Security.Jwt;
using GC.MFI.Security.Models;
using Microsoft.AspNetCore.Mvc;

namespace GC.MFI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SecurityController : ControllerBase
    {
        private IJwtTokenHelper authenticationHelper { get; }
       
        private readonly ILogger<SecurityController> _logger;
        public SecurityController(IJwtTokenHelper _authenticationHelper, ILogger<SecurityController> logger)
        {            
            this.authenticationHelper = _authenticationHelper;
            this._logger = logger;
        }

        [HttpPost]
        [Route("Authenticate")]
        public Tokens Authenticate(AuthenticationModel securityModel)
        {
            try
            {
                if (securityModel == null)
                    throw new Exception("Please enter userid and password to authenticate.");
                return   authenticationHelper.Authenticate(securityModel);
            }
            catch (Exception ex)
            {
                _logger.Log(LogLevel.Error, ex.Message);
                throw;
            }
        }
         
    }
}