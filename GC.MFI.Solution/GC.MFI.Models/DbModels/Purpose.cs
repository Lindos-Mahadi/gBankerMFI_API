using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GC.MFI.Models.DbModels.BaseModels;

namespace GC.MFI.Models.DbModels
{
    [Table("Purpose")]
    public partial class Purpose : LegacyDbModelBase, ILegacyDbModelBase
    {
        public int PurposeID { get; set; }

        [Required]
        [StringLength(10)]
        public string PurposeCode { get; set; }

        [Required]
        [StringLength(100)]
        public string PurposeName { get; set; }

        public bool? IsActive { get; set; }

        [Column(TypeName = "smalldatetime")]
        public DateTime? InActiveDate { get; set; }

        [Required]
        [StringLength(35)]
        public string CreateUser { get; set; }

        [Column(TypeName = "smalldatetime")]
        public DateTime CreateDate { get; set; }
        public int OrgID { get; set; }
        public string MainSector { get; set; }
        public string MainSectorName { get; set; }
        public string SubSector { get; set; }
        public string SubSectorName { get; set; }
    }
}
