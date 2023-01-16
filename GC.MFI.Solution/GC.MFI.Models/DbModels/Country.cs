using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Security.Principal;
using System.Text;
using System.Threading.Tasks;

namespace GC.MFI.Models.DbModels
{
    public class Country
    {
        public int CountryId { get; set; }
        public string CountryCode { get; set; }
        [Required]
        public string CountryName { get; set; }
        [Required]
        public string CountryShortCode { get; set; }
        [Required]
        public string isoCode3 { get; set; }
        [Required]
        public bool Status { get; set; }

    }
}
