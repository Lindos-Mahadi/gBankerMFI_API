using GC.MFI.DataAccess.InfrastructureBase;
using GC.MFI.DataAccess.Repository.Interfaces;
using GC.MFI.Models.DbModels;

namespace GC.MFI.DataAccess.Repository.Implementations;

public class FcmTokenRepository: RepositoryBase<FcmToken>, IFcmTokenRepository
{
    public FcmTokenRepository(IDatabaseFactory databaseFactory) : base(databaseFactory)
    {
    }
}