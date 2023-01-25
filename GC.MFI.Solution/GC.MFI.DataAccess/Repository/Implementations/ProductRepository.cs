using GC.MFI.DataAccess.InfrastructureBase;
using GC.MFI.DataAccess.Repository.Interfaces;
using GC.MFI.DataAccess.Repository.Pos.Interfaces;
using GC.MFI.Models.DbModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GC.MFI.DataAccess.Repository.Implementations
{
    public class ProductRepository : LegacyRepositoryBase<Product>, IProductRepository
    {

        public ProductRepository(IDatabaseFactory databaseFactory) : base(databaseFactory)
        {
        }

    }
}

