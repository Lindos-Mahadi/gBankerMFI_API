using GC.MFI.DataAccess.InfrastructureBase;
using GC.MFI.DataAccess.Repository.Interfaces;
using GC.MFI.Models.DbModels;

namespace GC.MFI.DataAccess.Repository.Implementations
{
    public class SavingsAccCloseRepository : RepositoryBase<SavingsAccClose>, ISavingsAccCloseRepository
    {
        public SavingsAccCloseRepository(IDatabaseFactory databaseFactory) : base(databaseFactory)
        {
        }

    }
}
