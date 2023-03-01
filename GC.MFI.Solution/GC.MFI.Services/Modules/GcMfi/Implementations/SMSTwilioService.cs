using GC.MFI.DataAccess.Repository.Interfaces;
using GC.MFI.Models.DbModels;
using GC.MFI.Models.Models;
using GC.MFI.Models.ViewModels;
using GC.MFI.Services.Modules.GcMfi.Interfaces;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Twilio;
using Twilio.Rest.Api.V2010.Account;
using Twilio.TwiML.Messaging;

namespace GC.MFI.Services.Modules.GcMfi.Implementations
{
    public class SMSTwilioService : ISMSTwilioService
    {
        private readonly ISMSLogTableService sMSLogTableService;
        private readonly IConfiguration configuration;

        public SMSTwilioService(IConfiguration configuration, ISMSLogTableService sMSLogTableService)
        {
            this.configuration = configuration;
            this.sMSLogTableService = sMSLogTableService;
        }

        public async Task<TwilioSMSModel> SendSMSAsync(string phoneNumber)
        {
            var smsSID = configuration["TwilioSMS:account_sid"];
            var smsAuth = configuration["TwilioSMS:auth_token"];
            var smsPhone = configuration["TwilioSMS:FromNumber"];
            var smsHashSalt = configuration["TwilioSMS:HashSalt"];

            // Generate a random number
            
            var _min = 1000;
            var _max = 9999;
            Random _rdm = new Random();
            var smsCode = _rdm.Next(_min, _max).ToString();

            string hashCode = BCrypt.Net.BCrypt.HashPassword(smsCode);

            TwilioClient.Init(smsSID, smsAuth);

            var message = MessageResource.Create(
                body: "This is your otp varification code : " + smsCode,
                from: smsPhone,
                to: phoneNumber
                );

            SMSLogTableViewModel smsLogVModel = new SMSLogTableViewModel
            {
                MobileNo = phoneNumber,
                Message = smsCode,
                SendDate = DateTime.UtcNow,
                Status = "P",
                CreateDate = DateTime.UtcNow,
                CreateUser = phoneNumber,
                UpdateDate = DateTime.UtcNow
            };
            sMSLogTableService.Create(smsLogVModel);

            return  new TwilioSMSModel { isSuccess = true };
        }


        public async Task<bool> ResponseSMSAync(string phoneNumber, string message)
        {
            var responsSMS = sMSLogTableService.GetMany(t => t.MobileNo == phoneNumber);

            if (responsSMS == null)
            {
                return false;
            }
            else
            {
                var getLast = responsSMS.OrderByDescending(t => t.SendDate).FirstOrDefault();
                if (getLast.Message == message)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
        }

    }
}
