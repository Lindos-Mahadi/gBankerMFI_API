using GC.MFI.Models.DbModels;
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
        private readonly IMemberToPHCMappingService memberToPHCMappingService;
        public GHealthSecurityService(IConfiguration configuration, IMemberToPHCMappingService memberToPHCMappingService)
        {
            this.configuration = configuration;
            this.memberToPHCMappingService = memberToPHCMappingService;
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
        public async Task<SignUpResponse> SignUp(GHealthSignUpViewModel entity)
        {
            using (var httpClient = new HttpClient())
            {
                var values = new Dictionary<string, string> {
                        { "api_key", configuration["GHealthApiKeys:apikey"]},
                        { "fullname", entity.fullname },
                        { "gender", entity.gender },
                        { "email", entity.email },
                        { "mobile", entity.mobile },
                        { "password", entity.password },
                        { "dob", entity.dob },
                        { "blood_group", entity.blood_group }};
                var content = JsonConvert.SerializeObject(values);
                httpClient.DefaultRequestHeaders.Accept.Add(new System.Net.Http.Headers.MediaTypeWithQualityHeaderValue("application/json"));
                httpClient.DefaultRequestHeaders.Add("Authorization", "Basic QW5kcm9pZDo0bmRyMGlkQXBwcw==");
                StringContent httpContent = new StringContent(content, Encoding.UTF8, "application/json");

                var response = await httpClient.PostAsync(configuration["GHealthApiKeys:signup"], httpContent);
                var responseString = await response.Content.ReadAsStringAsync();
                if (responseString.Contains("Wrong API Key"))
                {
                    return null;
                }
                if (responseString.Contains("The user already exists"))
                {
                    return null;
                }

                var verificationResponse = JsonConvert.DeserializeObject<SignUpResponse>(responseString);
                var map = new MemberToPHCMapping();
                map.Barcode = verificationResponse.barcode;
                map.MemberId = entity.MemberId;
                memberToPHCMappingService.Create(map);
                return verificationResponse;
            }
        }
    }
}
