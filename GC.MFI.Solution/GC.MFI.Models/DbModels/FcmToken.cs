namespace GC.MFI.Models.DbModels;

public class FcmToken : DbModelBase
{
    public string DeviceId { get; set; }

    public long UserId { get; set; }
    public AspNetUser User { get; set; }

    public bool IsMobile { get; set; }
}