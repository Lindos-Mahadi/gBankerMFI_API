﻿using GC.MFI.Models;
using GC.MFI.Models.DbModels;
using GC.MFI.Models.Modules.Distributions.Security;
using GC.MFI.Models.Modules.Security;
using GC.MFI.Security.Jwt;
using GC.MFI.Security.Models;
using GC.MFI.Services.Modules.Security.Interfaces;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace GC.MFI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SecurityController : ControllerBase
    {
        private IJwtTokenHelper authenticationHelper { get; }
        private IAuthenticationService authenticationService { get; }

        private readonly ILogger<SecurityController> _logger;
        public SecurityController(IJwtTokenHelper _authenticationHelper, ILogger<SecurityController> logger, IAuthenticationService authenticationService)
        {            
            this.authenticationHelper = _authenticationHelper;
            this.authenticationService = authenticationService;
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

        [HttpPost]
        [Route("signup")]
        public async Task<SignUpResponse> SignUp(SignUpModel model)
        {
            try
            {
               if (!ModelState.IsValid)
               {
                    var errors = ModelState.Select(x => x.Value.Errors)
                        .Where(y => y.Count > 0)
                        .ToList();
                }
                return await authenticationService.Create(model);

            }
            catch (Exception ex)
            {
                _logger.Log(LogLevel.Error, ex.Message);
                throw;
            }
        }

    }
}