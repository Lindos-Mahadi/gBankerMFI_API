﻿using GC.MFI.Helpers;
using GC.MFI.Models;
using GC.MFI.Models.DbModels;
using GC.MFI.Models.Modules.Distributions.Security;
using GC.MFI.Models.Modules.Security;
using GC.MFI.Models.ViewModels;
using GC.MFI.Security.Jwt;
using GC.MFI.Security.Models;
using GC.MFI.Services.Modules.Security.Interfaces;
using GC.MFI.Utility.Helpers;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Memory;
using System.Configuration;
using System.Net.Http.Headers;

namespace GC.MFI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SecurityController : ControllerBase
    {
        private IJwtTokenHelper authenticationHelper { get; }
        private IAuthenticationService authenticationService { get; }
        private readonly ILogger<SecurityController> _logger;
        private readonly IMemoryCache memoryCache;
        public SecurityController( IJwtTokenHelper _authenticationHelper,IMemoryCache memoryCache, ILogger<SecurityController> logger, IAuthenticationService authenticationService)
        {
            this.authenticationHelper = _authenticationHelper;
            this.authenticationService = authenticationService;
            this._logger = logger;
            this.memoryCache = memoryCache;
        }
        
        [HttpPost]
        [Route("Authenticate")]
        public async Task<IActionResult> Authenticate(AuthenticationModel securityModel)
        {
            try
            {
                if (!ModelState.IsValid)
                    throw new Exception("Please enter userid and password to authenticate.");
                var token = await authenticationHelper.Authenticate(securityModel);
                if(token == null)
                    return Unauthorized();
                return Ok(token);
            }
            catch (Exception ex)
            {
                _logger.Log(LogLevel.Error, ex.Message);
                throw;
            }
        }
        [AllowAnonymous]
        [HttpPost]
        [Route("signup")]
        public async Task<IActionResult> SignUp(SignUpModel model)
        {
            try
            {
               if (!ModelState.IsValid)
               {
                    var errors = ModelState.Select(x => x.Value.Errors)
                        .Where(y => y.Count > 0)
                        .ToList();
                }
                var response = await authenticationService.Create(model);
                if(response.isSuccess == true)
                    return Ok(response);
                return BadRequest(response);
                

            }
            catch (Exception ex)
            {
                _logger.Log(LogLevel.Error, ex.Message);
                throw;
            }
        }
        [AllowAnonymous]
        [HttpGet]
        [Route("checkusername")]
        public async Task<bool> UserNameCheck(string username)
        {
            try {
                if(ModelState.IsValid)
                {
                    var result = await authenticationService.CheckUserName(username);
                    return result;
                }else
                {
                    return false;
                }
            }
            catch(Exception ex)
            {
                _logger.Log(LogLevel.Error, ex.Message);
                throw;

            }
        }
        [Authorize]
        [HttpPost]
        [Route("changepassword")]
        public async Task<IActionResult> ChangePassword(ChangePasswordModel CPM)
        {
            try
            {
                var header = AuthenticationHeaderValue.Parse(Request.Headers["Authorization"]).Parameter;
                JwtTokenModel token = JwtTokenDecode.GetDetailsFromToken(header);
                var result = await authenticationService.ChangePassword(CPM , token.UserId);
                return Ok(result);
            }
            catch (Exception ex)
            {
                _logger.Log(LogLevel.Error, ex.Message);
                throw;

            }
            

        }
        [Authorize]
        [HttpPost("logout")]
        public async Task<IActionResult> LogOut()
        {
            var header = AuthenticationHeaderValue.Parse(Request.Headers["Authorization"]).Parameter;
            JwtTokenModel token = JwtTokenDecode.GetDetailsFromToken(header);
            var gettoken = memoryCache.Get(token.UserId);
            if (gettoken == null) { return Unauthorized(); }
            memoryCache.Remove(token.UserId);
            string message = "Log out successfully";
            return Ok(message);
        }



    }
}