using GC.MFI.Models.DbModels.BaseModels;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GC.MFI.Models.DbModels
{
    [Table("District")]
    public class District : LegacyDbModelBase, ILegacyDbModelBase
    {
        [Key]
        public int DistrictID { get; set; }
        public int DivisionID { get; set; }

        [StringLength(20)]
        public string DistrictCode { get; set; }

        [StringLength(50)]
        public string DistrictName { get; set; }

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
