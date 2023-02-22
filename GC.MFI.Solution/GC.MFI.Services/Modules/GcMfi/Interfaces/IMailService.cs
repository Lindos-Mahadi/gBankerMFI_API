using GC.MFI.Models.DbModels;
using GC.MFI.Models.Models;
using System.Threading.Tasks;
namespace GC.MFI.Services.Modules.GcMfi.Interfaces
{
    public interface IMailService
    {
        Task SendEmailAsync(MailRequest mailRequest);
        
    }
}
