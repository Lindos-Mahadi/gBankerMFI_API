using GC.MFI.Models;
using GC.MFI.Models.Modules.Distributions.Email;
using GC.MFI.Services.Modules.Email.Interfaces;
using System.Net;
using System.Net.Mail;
using System.Threading.Tasks;

namespace GC.MFI.Services.Modules.Email.Implementations
{
    public class EmailHelper: IEmailHelper
    {
        public EmailHelper()
        {

        }
        public async Task<AuthenticationResponseModel> SendEmailAsync(SendEmail emailObj)
        {
            string fromMail = "pos.bizzntek@gmail.com";
            string fromPassword = "vdkcdgvbqjnsvher";

            MailMessage message = new MailMessage();
            message.From = new MailAddress(fromMail);
            message.Subject = emailObj.subject;
            message.To.Add(new MailAddress(emailObj.to)); 
            message.Body = emailObj.body;
            message.IsBodyHtml = true;

            var smtpClient = new SmtpClient("smtp.gmail.com")
            {
                Port = 587,
                Credentials = new NetworkCredential(fromMail, fromPassword),
                EnableSsl = true,
            };
            smtpClient.Send(message);
            return new AuthenticationResponseModel () { isSuccesful=true , message= "Send Mail Successfully" };
        }
    }
}
