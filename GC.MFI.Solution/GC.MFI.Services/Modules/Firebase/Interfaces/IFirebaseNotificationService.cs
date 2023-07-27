using FirebaseAdmin.Messaging;
using System.Threading.Tasks;

namespace GC.MFI.Services.Modules.Firebase.Interfaces
{
    public interface IFirebaseNotificationService
    {
        Task<string> SendAsync(Message message);
    }
}