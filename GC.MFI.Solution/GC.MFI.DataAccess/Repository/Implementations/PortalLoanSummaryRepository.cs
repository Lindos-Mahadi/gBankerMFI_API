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
            IEnumerable<PortalLoanSummary> portalList = null;

            var totalElems = _context.PortalLoanSummary.Count(x => x.ApprovalStatus == true);
            //if (filter.search != null)
            //{
            //    portalList = _context.PortalLoanSummary.Where(filter.search);
            //}
            //else
            //{
            //    portalList = _context.PortalLoanSummary.Where(t => t.ApprovalStatus == true);
            //}
            //if (filter.page > 0)
            //{
            //    var count = portalList.Count();
            //    portalList = portalList.Skip((filter.page - 1) * filter.per_page).Take(filter.per_page);
            //    var totalPage = Convert.ToInt32(Math.Ceiling(((double)count / (double)filter.per_page)));
            //    return new PagedResponse<IEnumerable<PortalLoanSummary>>(portalList, filter.page, filter.per_page, count, totalPage);
            //}
            //else
            //{
            //    var count = DataContext.PortalLoanSummary.Count();
            //    var totalPage = Convert.ToInt32(Math.Ceiling(((double)count / (double)count)));
            //    return new PagedResponse<IEnumerable<PortalLoanSummary>>(portalList, filter.page, filter.per_page, count, totalPage);

            //}
            portalList = _context.PortalLoanSummary
                                    .Where(filter.search)
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
