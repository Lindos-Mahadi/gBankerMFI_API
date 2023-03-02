using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Policy;
using System.Text;
using System.Threading.Tasks;

namespace GC.MFI.Services.Modules.GcMfi.Interfaces
{
    public interface IHttpService<T> where T : class
    {
        Task<List<T>> GetListRequest(string url);
        Task<T> GetRequest(string url);
        Task<T> PostRequest(string url, Dictionary<string, string> postData);
    }
}
