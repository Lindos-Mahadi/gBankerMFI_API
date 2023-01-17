using GC.MFI.Models.DbModels;
using GC.MFI.Models.DbModels.BaseModels;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GC.MFI.Models.DbModels
{
    [Table("Union")]
    public partial class Union : LegacyDbModelBase, ILegacyDbModelBase
    {
        public int UnionID { get; set; }
        public int UpozillaID { get; set; }

        [StringLength(20)]
        public string UnionCode { get; set; }

        [StringLength(50)]
        public string UnionName { get; set; }

        public bool? IsActive { get; set; }

        [Column(TypeName = "smalldatetime")]
        public DateTime? InActiveDate { get; set; }

        [Required]
        [StringLength(15)]
        public string CreateUser { get; set; }

        [Column(TypeName = "smalldatetime")]
        public DateTime CreateDate { get; set; }
    }
    
}