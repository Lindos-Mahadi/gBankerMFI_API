using GC.MFI.DataAccess.InfrastructureBase;
using GC.MFI.DataAccess.Repository.Interfaces;
using GC.MFI.Models.DbModels;
using GC.MFI.Models.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GC.MFI.DataAccess.Repository.Implementations
{
    public class DistrictRepository : LegacyRepositoryBase<District>, IDistrictRepository
    {
        public DistrictRepository(IDatabaseFactory databaseFactory) : base(databaseFactory)
        {
        }
    }
}