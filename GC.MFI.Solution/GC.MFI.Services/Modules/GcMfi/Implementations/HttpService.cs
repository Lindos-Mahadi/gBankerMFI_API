using GC.MFI.Services.Modules.GcMfi.Interfaces;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;

namespace GC.MFI.Services.Modules.GcMfi.Implementations
{
    public class HttpService<T> : IHttpService<T>
        where T : class
    {
        public async Task<List<T>> GetListRequest(string url)
        {
            try
            {
                using (HttpClient client = new HttpClient())
                {
                    var responseString = await client.GetStringAsync(url);
                    var response = JsonConvert.DeserializeObject<List<T>>(responseString);
                    return response;
                }
            }
            catch(Exception ex)
            {
                throw ex;
            }
        }

        public async Task<T> GetRequest(string url)
        {
            try
            {
                using (HttpClient client = new HttpClient())
                {
                    var responseString = await client.GetStringAsync(url);
                    var response = JsonConvert.DeserializeObject<T>(responseString);
                    return response;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<T> PostRequest(string url, Dictionary<string, string> postData)
        {
            try
            {
                using (HttpClient client = new HttpClient())
                {
                    var content = new FormUrlEncodedContent(postData);

                    var response = await client.PostAsync(url, content);

                    var responseString = await response.Content.ReadAsStringAsync();
                    T responseObj = JsonConvert.DeserializeObject<T>(responseString);
                    return responseObj;
                }
            }
            catch(Exception ex)
            {
                throw ex;
            }
        }
    }
}
