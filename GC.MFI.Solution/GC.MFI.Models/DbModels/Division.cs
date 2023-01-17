using GC.MFI.Models.DbModels.BaseModels;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GC.MFI.Models.DbModels
{
    [Table("Division")]
    public class Division : LegacyDbModelBase, ILegacyDbModelBase
    {
        [StringLength(50)]
        public string DivisionCode { get; set; }
        [StringLength(150)]
        public string DivisionName { get; set; }
	}
}