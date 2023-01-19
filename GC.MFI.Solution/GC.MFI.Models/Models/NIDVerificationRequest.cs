using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GC.MFI.Models.Models
{
    public class NIDVerificationRequest
    {
        [Required]
        public string nidNumber { get; set; }
        [Required]
        public string dateOfBirth { get; set; }
        [Required]
        public bool englishTranslation { get; set; }
    }
}
