using GC.MFI.Models.DbModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using XenterSolution.Models.ViewModels;

namespace GC.MFI.Models.ViewModels
{
    public class SavingsAccCloseViewModel : ViewModelBase , IViewModelBase
    {
        public long MemberID { get; set; }
        public long OfficeID { get; set; }
        public long SavingAccountID { get; set; }
    }
}
