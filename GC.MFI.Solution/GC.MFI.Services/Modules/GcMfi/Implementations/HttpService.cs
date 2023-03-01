using GC.MFI.Services.Modules.GcMfi.Interfaces;
using Newtonsoft.Json.Linq;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;

namespace GC.MFI.Services.Modules.GcMfi.Implementations
{
    public class HttpService : IHttpService
    {
        public async Task<string> GetRequest(string url)
        {
            using(HttpClient client = new HttpClient()) 
            {
                var responseString = await client.GetStringAsync(url);
                return responseString;
            }
        }

        public async Task<string> PostRequest(string url, Dictionary<string, string> postData)
        {
            using (HttpClient client = new HttpClient())
            {
                var content = new FormUrlEncodedContent(postData);

                var response = await client.PostAsync(url, content);

                var responseString = await response.Content.ReadAsStringAsync();
                return responseString;
            }
        }
    }
}
