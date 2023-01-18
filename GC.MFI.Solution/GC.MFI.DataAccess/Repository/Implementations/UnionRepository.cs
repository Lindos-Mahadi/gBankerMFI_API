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
    public class UnionRepository : LegacyRepositoryBase<Union>, IUnionRepository
    {
        private readonly GBankerDbContext _context;
        public UnionRepository(IDatabaseFactory databaseFactory, GBankerDbContext context) : base(databaseFactory)
        {
            _context = context;
        }

        public async Task<IEnumerable<Union>> GetAllUnionName(string search)
        {
            if (!String.IsNullOrEmpty(search))
            {
                var unionList = _context.Union.Where(t => t.UnionName!.Contains(search) || t.UnionName.Trim()
                .Replace(" ", "").ToUpper()!.Contains(search.Trim().Replace(" ", "").ToUpper()));
                return unionList.Skip(0).Take(10);

            }
            return _context.Union.Skip(0).Take(10);
        }
    }
}
