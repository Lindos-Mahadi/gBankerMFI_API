using Microsoft.Extensions.Configuration;
using GC.MFI.Models.Models;
using GC.MFI.Services.Modules.GcMfi.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Reflection.Metadata;
using System.Security.Policy;
using Newtonsoft.Json;
using System.Net.Http.Json;

namespace GC.MFI.Services.Modules.GcMfi.Implementations
{
    public class NIDService : INIDService
    {
        private readonly IConfiguration configuration;

        public NIDService(IConfiguration configuration)
        {
            this.configuration = configuration;
        }

        public async Task<NIDVerificationResponse> GetNIDInfo(NIDVerificationRequest request)
        {
            try
            {
                using(var httpClient = new HttpClient())
                {
                    var jsonRequest = JsonConvert.SerializeObject(request);

                    HttpRequestMessage req = new HttpRequestMessage(HttpMethod.Post, "relativeAddress");
                    var content = new StringContent(jsonRequest,
                                                        Encoding.UTF8,
                                                        "application/json");

                    httpClient.DefaultRequestHeaders.Add("x-api-key", configuration["PorichoyKeys:ApiKey"]);
                    var response = await httpClient.PostAsync(configuration["PorichoyKeys:NIDAutofillUrl"], content);

                    var responseString = await response.Content.ReadAsStringAsync();

                    var verificationResponse = JsonConvert.DeserializeObject<NIDVerificationResponse>(responseString);

                    return verificationResponse;
                }
            }
            catch(Exception ex)
            {
                throw ex;
            }
        }
    }
}
