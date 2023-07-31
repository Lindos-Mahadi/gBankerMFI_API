using FirebaseAdmin;
using FirebaseAdmin.Messaging;
using GC.MFI.Models.Models;
using GC.MFI.Services.Modules.Firebase.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GC.MFI.Services.Modules.Firebase.Implementations
{
    public class FirebaseNotificationService : IFirebaseNotificationService
    {
        private readonly FirebaseApp firebaseApp;

        public FirebaseNotificationService(FirebaseApp firebaseApp)
        {
            this.firebaseApp = firebaseApp;
        }


        public async Task<string> SendAsync(Message message)
        {
            // Send the message
            var response = await FirebaseMessaging.DefaultInstance.SendAsync(message);

            return response;
        }
    }
}
