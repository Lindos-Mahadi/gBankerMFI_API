using GC.MFI.DataAccess.InfrastructureBase;
using GC.MFI.DataAccess.Repository.Interfaces;
using GC.MFI.Models.DbModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GC.MFI.DataAccess.Repository.Implementations
{
    public class PurposeRepository : LegacyRepositoryBase<Purpose>, IPurposeRepository
    {
        public PurposeRepository(IDatabaseFactory databaseFactory, GBankerDbContext context) : base(databaseFactory)
        {
        }

        public async Task<IEnumerable<Purpose>> GetAllPurpose(string search)
        {
            if (!String.IsNullOrEmpty(search))
            {
                var officeList = DataContext.Purpose.Where(t => t.PurposeName!.Contains(search) || t.PurposeName.Trim().Replace(" ", "").ToUpper()!.Contains(search.Trim().Replace(" ", "").ToUpper()));
                return officeList.Skip(0).Take(10);
            }
            return DataContext.Purpose.Skip(0).Take(10);
        }
    }
}
