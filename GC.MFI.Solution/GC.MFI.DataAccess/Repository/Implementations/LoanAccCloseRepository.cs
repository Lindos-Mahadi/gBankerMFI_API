using GC.MFI.DataAccess.InfrastructureBase;
using GC.MFI.DataAccess.Repository.Interfaces;
using GC.MFI.Models.DbModels;

namespace GC.MFI.DataAccess.Repository.Implementations
{
    public class LoanAccCloseRepository : RepositoryBase<LoanAccClose>, ILoanAccCloseRepository
    {
        public LoanAccCloseRepository(IDatabaseFactory databaseFactory) : base(databaseFactory)
        {
        }

    }
}
