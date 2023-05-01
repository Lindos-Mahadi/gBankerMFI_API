using GC.MFI.DataAccess.Repository.Implementations;
using GC.MFI.DataAccess.Repository.Interfaces;
using GC.MFI.Models.Models;
using GC.MFI.Models.ViewModels;
using GC.MFI.Services.Modules.GcMfi.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GC.MFI.Services.Modules.GcMfi.Implementations
{
    public class EmailService : IEmailService
    {
         private readonly IMailService mailService;
        private readonly IEmailLogTableService emailLogTableService;
        public EmailService(IMailService mailService, IEmailLogTableService emailLogTableService) 
        {
            this.mailService = mailService;
            this.emailLogTableService = emailLogTableService;
        }

        public async Task<EmailResponse> SendOtp(string email)
        {
            var _min = 1000;
            var _max = 9999;
            Random _rdm = new Random();
            var otpCode = _rdm.Next(_min, _max).ToString();
            string mailBody = $"Dear Sir,<br><br>Thank you for using our service. Your one-time OTP is: <b> {otpCode}</b>.<br><br>Please enter this OTP to verify your account.<br><br>This OTP is valid for 3 minutes only. If you did not request this OTP, please ignore this email.<br><br>Thank you,<br>Grameen Communication";
            EmailLogTableViewModel emailLogVModel = new EmailLogTableViewModel
            {
                Email = email,
                Message = otpCode,
                SendDate = DateTime.UtcNow,
                Status = "P",
                CreateDate = DateTime.UtcNow,
                CreateUser = "",
                UpdateDate = DateTime.UtcNow
            };
            MailRequest mailRequest = new MailRequest
            {
                ToEmail = email,
                Subject = "Verify Your Email",
                Body = mailBody
            };
            var findMail = emailLogTableService.GetMany(t=> t.Email.ToUpper()  == email.ToUpper()).OrderByDescending(t=> t.SendDate).FirstOrDefault();
            if (findMail != null && findMail.SendDate.AddMinutes(3) >= DateTime.UtcNow ) 
            {
                return new EmailResponse { isSuccess = false, Message = "pleas...wait until 3 minutes if you are not get a otp message. " };
            }
            emailLogTableService.Create(emailLogVModel);
            await mailService.SendEmailAsync(mailRequest);
            return new EmailResponse { isSuccess = true, Message = "Code send successfully." };

        }

        public async Task<EmailResponse> VerifyEmailOtpAsync(string email, string message)
        {
            var findMail = emailLogTableService.GetMany(t => t.Email.ToUpper() == email.ToUpper() && t.Message == message).OrderByDescending(t => t.SendDate).FirstOrDefault();

            if (findMail == null)
            {
                return new EmailResponse { Message = "Please Input a valid otp." };
            }
            if (findMail.Status == "V")
            {
                return new EmailResponse { Message = "This otp  is already verified!" };
            }
            else
            {
                // check if send datetime is greater than(5 mins) datetime now // send response expired

                if (findMail.SendDate.AddMinutes(3) >= DateTime.UtcNow)
                {
                    findMail.Status = "V";
                    emailLogTableService.Update(findMail);

                    return new EmailResponse { isSuccess = true, Message = "Email Verified Successfully" };
                }
                else
                {
                    return new EmailResponse { isSuccess = false, Message = "Oops! Your Otp's time expired. Please send otp again..." };
                }
            }
        }
    }
}
