using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GC.MFI.Models.Models
{
    public class NIDVerificationRequest
    {
        public string nidNumber { get; set; }
        public string dateOfBirth { get; set; }
        public bool englishTranslation { get; set; }
    }
}
