using GC.MFI.Models.DbModels;
using GC.MFI.Models.DbModels.BaseModels;
using GC.MFI.Models.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GC.MFI.Models.DbModels
{

    [Table("NIDLogging")]
    public class NIDLogging : LegacyDbModelBase, ILegacyDbModelBase
    {
        [Key]
        public long LoggingId { get; set; }
        public string ClientIP { get; set; }
      //  [Required]
        public string Request { get; set; }
      //  [Required]
        public string Response { get; set; }
        //   [Required]

        [Column(TypeName = "smalldatetime")]
        public DateTime Datetime { get; set; }
    }
}
