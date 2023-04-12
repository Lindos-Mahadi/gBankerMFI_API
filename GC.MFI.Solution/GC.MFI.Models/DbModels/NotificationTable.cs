using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GC.MFI.Models.DbModels
{
    public class NotificationTable : DbModelBase, IDbModelBase
    {
        public string Message { get; set; }
        public string SenderType { get; set; }
        public long? SenderID { get; set; }
        public string ReceiverType { get; set; }
        public long? ReceiverID { get; set; }
        public bool Sms { get; set; }
        public bool Email { get; set; }
        public bool Push { get; set; }
    }
}
