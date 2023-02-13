using GC.MFI.Models.DbModels;

using XenterSolution.Models.ViewModels;

namespace GC.MFI.Models.ViewModels
{
    public class LoanAccRescheduleViewModel : ViewModelBase , IViewModelBase
    {   public long MemberID { get; set; }
        public long OfficeID { get; set; }
        public long LoanID { get; set; }
    }
}
