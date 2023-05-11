using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GC.MFI.Models.ViewModels
{
    public class NomineeXSavingsSummaryViewModel
    {
        public long SavingSummaryID { get; set; }
        public string NomineeName { get; set; }
        public string NFatherName { get; set; }
        public string NRelationName { get; set; }
        public string NAddressName { get; set; }
        public int? NAlocation { get; set; }
        public long ImageId { get; set; }
        public string Image { get; set; }
        public long NIDId { get; set; }
        public string NID { get; set; }
    }
}
