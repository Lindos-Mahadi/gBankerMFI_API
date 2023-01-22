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
    public class PortalLoanSummaryRepository : LegacyRepositoryBase<PortalLoanSummary>, IPortalLoanSummaryRepository
    {
        private readonly GBankerDbContext _context;
        public PortalLoanSummaryRepository(IDatabaseFactory databaseFactory, GBankerDbContext context) : base(databaseFactory)
        {
            this._context = context;
        }

        public void Create(PortalLoanSummary entity)
        {
            _context.PortalLoanSummary.Add(entity);
        }

        public IEnumerable<PortalLoanSummary> GetAllPortalLoanSummary()
        {
            return _context.PortalLoanSummary;
        }

        //public async Task<IEnumerable<Member>> GetAllMember(string search)
        //{
        //    if (!String.IsNullOrEmpty(search))
        //    {
        //        var memberList = DataContext.Member.Where(t => t.FirstName!.Contains(search) || t.FirstName.Trim()
        //        .Replace(" ", "").ToUpper()!.Contains(search.Trim().Replace(" ", "").ToUpper()));
        //        return memberList.Skip(0).Take(10);

        //    }
        //    return DataContext.Member.Skip(0).Take(10);
        //}
    }
}
