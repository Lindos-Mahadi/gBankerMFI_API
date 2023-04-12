using GC.MFI.Models.DbModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using XenterSolution.Models.ViewModels;

namespace GC.MFI.Models.ViewModels
{
    public class NotificationTableViewModel : ViewModelBase, IViewModelBase
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
