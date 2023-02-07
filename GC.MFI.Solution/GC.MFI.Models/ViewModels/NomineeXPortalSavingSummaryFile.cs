using GC.MFI.Models.DbModels;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GC.MFI.Models.ViewModels
{
    public class NomineeXPortalSavingSummaryFile
    {
        [Key]
        public long PortalMemberNomineeId { get; set; }

        public string NomineeName { get; set; }
        public string NFatherName { get; set; }
        public string NRelationName { get; set; }
        public string NAddressName { get; set; }
        public int? NAlocation { get; set; }
        public string NIDNumber { get; set; }
        public string Image { get; set; }
        public string Nid { get; set; }

        public long PortalSavingSummaryID { get; set; }
    }
}
