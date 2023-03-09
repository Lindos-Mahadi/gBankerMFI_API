using GC.MFI.Models.DbModels.BaseModels;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GC.MFI.Models.DbModels
{
    public class MemberToPHCMapping : LegacyDbModelBase, ILegacyDbModelBase
    {
        [Key]
        public long MemberToPHCId { get; set; }
        public long MemberId { get; set; }
        public string Barcode { get; set; }
    }
}
