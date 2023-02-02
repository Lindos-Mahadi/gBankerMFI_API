using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GC.MFI.Models.DbModels
{
    public class PortalSavingFileUpload
    {
        [Key]
        public long FileUploadId { get; set; }
        public string EntityName { get; set; }
        public long EntityId { get; set; }
        public string PropertyName { get; set; }
        public string File { get; set; }
        public string FileName { get; set; }
        public string Type { get; set; }
    }
}
