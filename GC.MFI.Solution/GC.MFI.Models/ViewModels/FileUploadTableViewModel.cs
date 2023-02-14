using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GC.MFI.Models.ViewModels
{
    public class FileUploadTableViewModel
    {
        public long FileUploadId { get; set; }
        public string EntityName { get; set; }
        public long EntityId { get; set; }
        public string PropertyName { get; set; }
        public string FileUrl { get; set; }
        public string FileName { get; set; }
        public string Type { get; set; }
        public string DocumentType { get; set; }
    }
}
