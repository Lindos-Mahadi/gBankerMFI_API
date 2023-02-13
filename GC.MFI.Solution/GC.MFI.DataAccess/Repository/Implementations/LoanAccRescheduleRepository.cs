using GC.MFI.DataAccess.InfrastructureBase;
using GC.MFI.DataAccess.Repository.Interfaces;
using GC.MFI.Models.DbModels;

namespace GC.MFI.DataAccess.Repository.Implementations
{
    public class LoanAccRescheduleRepository : RepositoryBase<LoanAccReschedule>, ILoanAccRescheduleRepository
    {
        public LoanAccRescheduleRepository(IDatabaseFactory databaseFactory) : base(databaseFactory)
        {
        }

    }
}
