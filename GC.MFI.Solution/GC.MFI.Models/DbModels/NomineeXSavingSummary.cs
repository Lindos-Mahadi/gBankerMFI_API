using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace GC.MFI.Models.DbModels
{
    public class NomineeXSavingSummary
    {
        [Key]
        public long SavingSummaryID { get; set; }
        public string NomineeName { get; set; }
        public string NFatherName { get; set; }
        public string NRelationName { get; set; }
        public string NAddressName { get; set; }
        public int? NAlocation { get; set; }
        public string NIDNumber { get; set; }
        public long ImageId { get; set; }
        public long NIDId { get; set; }
        [JsonIgnore]
        public SavingSummary SavingSummary { get; set; }

    }
}
