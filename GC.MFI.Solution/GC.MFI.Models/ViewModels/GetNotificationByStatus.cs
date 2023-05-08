using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GC.MFI.Models.ViewModels
{
    public class GetNotificationByStatus
    {
        public long Id { get; set; }
        public string Message { get; set; }
        public string SenderType { get; set; }
        public long? SenderID { get; set; }
        public string ReceiverType { get; set; }
        public long? ReceiverID { get; set; }
        public bool Sms { get; set; }
        public bool Email { get; set; }
        public bool Push { get; set; }
        public string Status { get; set; }
        public string ConnID { get; set; }
        public string CreateUser { get; set; }
        public DateTime? CreateDate { get; set; }
        public string UpdateUser { get; set; }
        public DateTime? UpdateDate { get; set; }
        
    }
}
