using XenterSolution.Models.ViewModels;

namespace GC.MFI.Models.ViewModels;

public class FcmTokenViewModel : ViewModelBase
{
    public string DeviceId { get; set; }
    public string UserId { get; set; }
    public bool IsMobile { get; set; }
}

public class FcmTokenInputModel
{
    public string DeviceId { get; set; }
    public bool IsMobile { get; set; }
}