using System.Threading.Tasks;
using GC.MFI.Models.DbModels;
using GC.MFI.Models.ViewModels;

namespace GC.MFI.Services.Modules.Firebase.Interfaces;

public interface IFcmTokenService
{
    Task<FcmTokenViewModel> CreateOrUpdate(FcmTokenViewModel input);
    Task<FcmTokenViewModel> GetByUserId(long userId);
}