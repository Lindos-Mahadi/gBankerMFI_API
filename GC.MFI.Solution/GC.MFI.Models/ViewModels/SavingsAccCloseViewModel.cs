using GC.MFI.Models.DbModels;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using XenterSolution.Models.ViewModels;

namespace GC.MFI.Models.ViewModels
{
    public class SavingsAccCloseViewModel : ViewModelBase , IViewModelBase
    {
        [Required]
        public long MemberID { get; set; }
        [Required]
        public long OfficeID { get; set; }
        [Required]
        public long SavingAccountID { get; set; }
    }
}
