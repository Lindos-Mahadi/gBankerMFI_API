using GC.MFI.DataAccess.InfrastructureBase;
using GC.MFI.DataAccess.Repository.Interfaces;
using GC.MFI.Models.DbModels;

namespace GC.MFI.DataAccess.Repository.Implementations
{
    public class LoggerRepository : RepositoryBase<Logger>, ILoggerRepository
    {
        public LoggerRepository(IDatabaseFactory databaseFactory) : base(databaseFactory)
        {
        }

    }
}
