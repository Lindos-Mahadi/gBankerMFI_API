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
    public class PortalMemberNominee : LegacyDbModelBase, ILegacyDbModelBase
    {
        [Key]
        public long PortalMemberNomineeId { get; set; }

        public string NomineeName { get; set; }
        public string NomineeFather { get; set; }
        public string NomineeMother { get; set; }
        public string NomineeSpouseName { get; set; }
        public string NomineeNID { get; set; }
        public string NomineeRelation { get; set; }
        [Column(TypeName = "smalldatetime")]
        public DateTime? NomineeDateOfBirth { get; set; }
    
        [StringLength(15)]
        public string CreateUser { get; set; }

        [Column(TypeName = "smalldatetime")]
        public DateTime CreateDate { get; set; }
        public long PortalSavingSummaryId { get; set; }
        [JsonIgnore]
        public PortalSavingSummary PortalSavingSummary { get; set; }
    }
}
