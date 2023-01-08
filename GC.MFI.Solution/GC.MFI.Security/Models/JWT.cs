using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GC.MFI.Security.Models
{
    public class JWT : IJWT
    {
        public string Key { get; set; } = "mnakdfkal1d2121212efeAbcMyz";
        public string Issuer { get; set; }
        public string Audience { get; set; }
    }
    public interface IJWT
    {
        string Key { get; set; }
        string Issuer { get; set; }
        string Audience { get; set; }
    }
}
