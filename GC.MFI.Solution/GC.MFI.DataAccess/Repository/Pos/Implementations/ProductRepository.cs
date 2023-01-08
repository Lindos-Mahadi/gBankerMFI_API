using GC.MFI.DataAccess.InfrastructureBase;
using GC.MFI.DataAccess.Repository.Pos.Interfaces;
using GC.MFI.Models.DbModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GC.MFI.DataAccess.Repository.Pos.Implementations
{
    public class ProductRepository : RepositoryBase<Product>, IProductRepository
    {

        public ProductRepository(IDatabaseFactory databaseFactory) : base(databaseFactory)
        {
        }
 
    }
}

