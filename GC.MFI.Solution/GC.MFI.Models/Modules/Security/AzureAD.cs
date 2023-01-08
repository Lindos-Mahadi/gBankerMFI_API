using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GC.MFI.Models
{
    public class AzureAD
    {
        public string ClientId { get; set; }
        public string ClientSecret { get; set; }
        public string TenantId { get; set; }
        public string Instance { get; set; }
        public string GraphResource { get; set; }
        public string GraphResourceEndPoint { get; set; }
        public string CrmUser { get; set; }
        public string CrmPassword { get; set; }
        public string CrmApiUrl { get; set; }
    }
}
