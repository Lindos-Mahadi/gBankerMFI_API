using GC.MFI.DataAccess.InfrastructureBase;
using GC.MFI.DataAccess.Repository.Interfaces;
using GC.MFI.Models;
using GC.MFI.Models.DbModels;
using GC.MFI.Models.ViewModels;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Data.Entity;
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
        public async Task<PagedResponse<IEnumerable<PortalLoanSummary>>> GetAllPortalLoanSummaryPaged(PaginationFilter<PortalLoanSummary> filter)
        {

            var totalElems = _context.PortalLoanSummary.Count(x => x.ApprovalStatus == true);
            var portalList = _context.PortalLoanSummary
                                    .Where(filter.search)
                                    .Where(x => x.ApprovalStatus == true)
                                    .Skip(filter.pageNum > 0 ? (filter.pageNum - 1) * filter.pageSize : 0)
                                    .Take(filter.pageSize);

            return new PagedResponse<IEnumerable<PortalLoanSummary>>(
                portalList,
                filter.pageNum,
                filter.pageSize,
                totalElems,
                totalElems / filter.pageSize);
            
        }
    }
}
