using GC.MFI.Models.Modules.Distributions.Email;
using GC.MFI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GC.MFI.Services.Modules.Email.Interfaces
{
    public interface IEmailHelper
    {
     Task<AuthenticationResponseModel> SendEmailAsync(SendEmail emailObj);

    }
}
