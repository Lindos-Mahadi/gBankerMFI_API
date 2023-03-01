using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Policy;
using System.Text;
using System.Threading.Tasks;

namespace GC.MFI.Services.Modules.GcMfi.Interfaces
{
    public interface IHttpService
    {
        Task<string> GetRequest(string url);
        Task<string> PostRequest(string url, Dictionary<string, string> postData);
    }
}
