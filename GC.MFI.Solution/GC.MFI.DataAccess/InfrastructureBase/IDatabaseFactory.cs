using GC.MFI.DataAccess;
using GC.MFI.Models.DbModels;

namespace GC.MFI.DataAccess.InfrastructureBase
{
    public interface IDatabaseFactory
    {
      // ApplicationDbContext Get();
       BntPOSContext Get();
    }
}
