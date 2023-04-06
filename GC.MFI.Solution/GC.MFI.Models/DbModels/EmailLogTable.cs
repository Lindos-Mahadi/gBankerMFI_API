using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GC.MFI.Models.DbModels
{
    public class EmailLogTable : DbModelBase
    {
        [Required]
        public string Email { get; set; }
        [Required]
        public DateTime SendDate { get; set; }
        [Required]
        public string Message { get; set; }
    }
}
