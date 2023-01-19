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
    public class Investor : LegacyDbModelBase, ILegacyDbModelBase
    {
        public byte InvestorID { get; set; }

        [Required]
        [StringLength(5)]
        public string InvestorCode { get; set; }

        [Required]
        [StringLength(50)]
        public string InvestorName { get; set; }

        public bool IsActive { get; set; }

        [Column(TypeName = "smalldatetime")]
        public DateTime? InActiveDate { get; set; }

        [Required]
        [StringLength(35)]
        public string CreateUser { get; set; }

        [Column(TypeName = "smalldatetime")]
        public DateTime CreateDate { get; set; }
        public int OrgID { get; set; }
    }
}
