using FirebaseAdmin;
using Google.Apis.Auth.OAuth2;
using Microsoft.Extensions.DependencyInjection;

namespace GC.MFI.Services.Modules.Firebase.Implementations
{
    public static class FirebaseAppSingleton
    {

        public static void AddFirebaseAppSingleton(this IServiceCollection services, string keyFilePath)
        {
            FirebaseApp app = FirebaseApp.Create(new AppOptions()
            {
                Credential = GoogleCredential.FromFile(keyFilePath),
            });

            services.AddSingleton(app);
        }
    }
}
