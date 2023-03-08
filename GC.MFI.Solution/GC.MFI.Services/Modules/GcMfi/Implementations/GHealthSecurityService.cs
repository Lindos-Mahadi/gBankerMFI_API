using GC.MFI.Models.Models;
using GC.MFI.Models.ViewModels;
using GC.MFI.Services.Modules.GcMfi.Interfaces;
using GC.MFI.Utility.Helpers;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace GC.MFI.Services.Modules.GcMfi.Implementations
{
    public class GHealthSecurityService : IGHealthSecurityService
    {
        private readonly IConfiguration configuration;
        public GHealthSecurityService(IConfiguration configuration)
        {
            this.configuration = configuration;
        }

        public async Task<GHealthSignViewModel> Authenticate(string username, string password)
        {
            try
            {
                var user_name = GHealthHelper.Base64Encode(username);
                var pass = GHealthHelper.Base64Encode(password);
                using (var httpClient = new HttpClient())
                {
                    var values = new Dictionary<string, string> {
                        { "user_name", user_name},
                        { "password", pass }};
                    var content = new FormUrlEncodedContent(values);

                    var response = await httpClient.PostAsync("http://ghealth.gramweb.net/api/sign_in", content);
                    var responseString = await response.Content.ReadAsStringAsync();
                    if (responseString.Contains("message"))
                    {
                        return null;
                    }
                    var verificationResponse = JsonConvert.DeserializeObject<GHealthSignViewModel>(responseString);

                    return verificationResponse;
                }
            }
            catch(Exception ex)
            {
                throw ex;
            }
        }

        public async Task<List<GHealthPatientViewModel>> IsGHealthLoggedIn(string mobile)
        {
            try
            {
                using(var httpClient = new HttpClient()) {
                    var values = new Dictionary<string, string> {
                        { "apikey", configuration["GHealthApiKeys:apikey"]},
                        { "mobile", mobile }};
                    var content = new FormUrlEncodedContent(values);

                    var response = await httpClient.PostAsync(configuration["GHealthApiKeys:url"], content);
                    var responseString = await response.Content.ReadAsStringAsync();
                    if(responseString.Contains("message"))
                    {
                        return null;
                    }
                    var verificationResponse = JsonConvert.DeserializeObject<List<GHealthPatientViewModel>>(responseString);
                    
                    return verificationResponse;
                }

            }catch(Exception ex)
            {
                throw ex;
            }
        }
    }
}
