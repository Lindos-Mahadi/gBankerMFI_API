using GC.MFI.Models.DbModels.BaseModels;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Text.Json.Serialization;

namespace GC.MFI.Models.DbModels
{
    public class NomineeXPortalSavingSummary : LegacyDbModelBase, ILegacyDbModelBase
    {
        [Key]
        public long PortalMemberNomineeId { get; set; }

        public string NomineeName { get; set; }
        public string NFatherName { get; set; }
        public string NRelationName { get; set; }
        public string NAddressName { get; set; }
        public int? NAlocation { get; set; }

        public long PortalSavingSummaryID { get; set; }
        [JsonIgnore]
        public PortalSavingSummary PortalSavingSummary { get; set; }
    }
}
