using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GC.MFI.Models
{
    public class MisSetting : IMisSetting
    {
        public string CORSEnabledUrl { get; set; } = "";
        public string ObservationTemplateId { get; set; } = "";
        public string AppServicePortalUrl { get; set; } = "";
        public string BAATemplateName { get; set; } = "";
        public string TeamsAppId { get; set; } = "";
        public string EnvironmentName { get; set; } = "";
        public string ApplicationVersion { get; set; } = "";
        public string TenantConfigEntityTypeCode { get; set; } = "100";
        public string SampleServiceApptId { get; set; } = "00";
        public string TenantPaymentDescription { get; set; } = "";
 

    }
    public interface IMisSetting
    {
        string CORSEnabledUrl { get; set; }
        public string ObservationTemplateId { get; set; }
        public string AppServicePortalUrl { get; set; } 
        public string BAATemplateName { get; set; }
        public string TeamsAppId { get; set; } 
        public string EnvironmentName { get; set; } 
        public string ApplicationVersion { get; set; } 
        public string TenantConfigEntityTypeCode { get; set; } 
        public string SampleServiceApptId { get; set; } 
        public string TenantPaymentDescription { get; set; }
    }
}
