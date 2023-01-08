using GC.MFI.DataAccess.InfrastructureBase;
using GC.MFI.DataAccess.Repository.Pos.Interfaces;
using GC.MFI.Models.DbModels;

namespace GC.MFI.DataAccess.Repository.Pos.Implementations
{
    public class OrderRepository : RepositoryBase<Order>, IOrderRepository
    {

        public OrderRepository(IDatabaseFactory databaseFactory) : base(databaseFactory)
        {
        }      
    }
}
