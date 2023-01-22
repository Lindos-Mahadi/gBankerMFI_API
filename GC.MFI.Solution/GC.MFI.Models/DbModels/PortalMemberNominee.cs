using GC.MFI.Models.DbModels.BaseModels;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GC.MFI.Models.DbModels
{
    public class PortalMemberNominee : LegacyDbModelBase, ILegacyDbModelBase
    {
        public long MemberNomineeId { get; set; }
        public long MemberId { get; set; }

        [StringLength(250)]
        public string NomineeName { get; set; }
        public string NomineeFather { get; set; }
        public string NomineeMother { get; set; }
        public string NomineeSpouseName { get; set; }
        public string NomineeNID { get; set; }
        public string NomineeRelation { get; set; }
        [Column(TypeName = "smalldatetime")]
        public DateTime? NomineeDateOfBirth { get; set; }
        public string NomineeAddress { get; set; }
        [Column(TypeName = "varchar(MAX)")]
        public string NomineePhoto { get; set; }
        [Column(TypeName = "varchar(MAX)")]
        public string ApplicantSignature { get; set; }
        [Column(TypeName = "smalldatetime")]
        public DateTime? ApplicationDate { get;set; }


        [Required]
        [StringLength(15)]
        public string CreateUser { get; set; }

        [Column(TypeName = "smalldatetime")]
        public DateTime CreateDate { get; set; }
    }
}
