using GC.MFI.Models.DbModels;
using GC.MFI.Models.Models;
using GC.MFI.Services.Modules.GcMfi.Interfaces;
using GC.MFI.Services.Modules.Security.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;
using Twilio;
using Twilio.Rest.Api.V2010.Account;
using Twilio.Types;
using GC.MFI.Security.Models;
using GC.MFI.Models.ViewModels;
using Twilio.TwiML.Messaging;

namespace GC.MFI.WebApi.Controllers.Modules.GcMfi
{
    [ApiController]
    [Route("api/[controller]")]
    public class NotificationController : Controller
    {
        private readonly IMailService mailService;
        private readonly ISMSTwilioService sMSTwilioService;
        private readonly ISMSLogTableService sMSLogTableService;
        private readonly IEmailService emailService;
        public NotificationController(IMailService mailService, IEmailService emailService, ISMSTwilioService sMSTwilioService, ISMSLogTableService sMSLogTableService)
        {
            this.mailService = mailService;
            this.sMSTwilioService = sMSTwilioService;
            this.sMSLogTableService = sMSLogTableService;
            this.emailService = emailService;
        }

        [HttpPost("Send")]
        public async Task<IActionResult> Send([FromForm] MailRequest request)
        {
            try
            {
                await mailService.SendEmailAsync(request);
                return Ok();
            }
            catch (Exception ex)
            {

                throw ex;
            }

        }

        [HttpPost("SendOtp")]
        public async Task<IActionResult> SendOtp(string mobileNo)
        {
            try
            {
                var sendMessage= await sMSTwilioService.SendOtpAsync(mobileNo);
                return Ok(sendMessage);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        [HttpPost("VerifyOtp")]
        public async Task<IActionResult> VerifyOtp(string mobileNo, string message)
        {
            try
            {

                var getMessage = await sMSTwilioService.VerifyOtpAsync(mobileNo, message);
                return Ok(getMessage);
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        [HttpPost("SendEmailOtp")]
        public async Task<IActionResult> SendEmailOtp(string email)
        {
            try
            {
                var sendMessage = await emailService.SendOtp(email);
                return Ok(sendMessage);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        [HttpPost("VerifyEmailOtp")]
        public async Task<IActionResult> VerifyEmailOtp(string email, string message)
        {
            try
            {

                var getMessage = await emailService.VerifyEmailOtpAsync(email, message);
                return Ok(getMessage);
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

    }
}
