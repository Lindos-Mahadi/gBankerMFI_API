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

        public async Task<TwilioSMSModel> SendOtpAsync(string mobileNo)
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

            SMSLogTableViewModel smsLogVModel = new SMSLogTableViewModel
            {
                MobileNo = mobileNo,
                Message = smsCode,
                SendDate = DateTime.UtcNow,
                Status = "P",
                CreateDate = DateTime.UtcNow,
                CreateUser = mobileNo,
                UpdateDate = DateTime.UtcNow
            };

            var number = sMSLogTableService.GetMany(t => t.MobileNo == mobileNo).OrderByDescending(t=> t.SendDate).FirstOrDefault();
            if (number != null && smsLogVModel.SendDate.AddMinutes(3) <= DateTime.UtcNow)
            {
                return  new TwilioSMSModel { isSuccess = false, Message = "pleas...wait until 3 minutes if you are not get a otp message." };
            }

            TwilioClient.Init(smsSID, smsAuth);

            var message = MessageResource.Create(
                body: "This is your otp varification code : " + smsCode,
                from: smsPhone,
                to: mobileNo
                );

            sMSLogTableService.Create(smsLogVModel);
            return new TwilioSMSModel { isSuccess = true, Message = "Code send successfully." };


        }


        public async Task<TwilioSMSModel> VerifyOtpAsync(string mobileNo, string message)
        {
            var responsSMS = sMSLogTableService.GetMany(s => s.MobileNo == mobileNo && s.Message == message).OrderByDescending(t =>t.SendDate).FirstOrDefault();
           
            if (responsSMS == null)
            {
                return new TwilioSMSModel { Message = "Please Input a valid otp." };
            }
            if (responsSMS.Status.Trim() == "V")
            {
                return new TwilioSMSModel { Message = "This otp  is already verified!" };
            }
            else
            {
                // check if send datetime is greater than(5 mins) datetime now // send response expired

                if (responsSMS.SendDate.AddMinutes(3) >= DateTime.UtcNow)
                {
                    responsSMS.Status = "V";
                    sMSLogTableService.Update(responsSMS);

                    return new TwilioSMSModel { isSuccess = true, Message = "Number Verified Successfully" };
                }
                else
                {
                    return new TwilioSMSModel {isSuccess = false, Message = "Oops! Your Otp's time expired. Please send otp again..." };
                }
            }
        }

    }
}
