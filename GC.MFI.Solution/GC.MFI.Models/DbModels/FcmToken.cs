using GC.MFI.Models.Modules.Distributions.Security;

namespace GC.MFI.Models.DbModels;

public class FcmToken : DbModelBase
{
    public string DeviceId { get; set; }

    public string UserId { get; set; }
    public ApplicationUser User { get; set; }

    public bool IsMobile { get; set; }
}