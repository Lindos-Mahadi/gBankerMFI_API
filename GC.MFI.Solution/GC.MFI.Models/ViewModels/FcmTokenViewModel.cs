using XenterSolution.Models.ViewModels;

namespace GC.MFI.Models.ViewModels;

public class FcmTokenViewModel: ViewModelBase
{
    public string DeviceId { get; set; }

    public long UserId { get; set; }

    public bool IsMobile { get; set; }
}