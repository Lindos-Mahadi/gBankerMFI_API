using GC.MFI.DataAccess.InfrastructureBase;
using GC.MFI.DataAccess.Repository.Interfaces;
using GC.MFI.Models;
using GC.MFI.Models.DbModels;
using GC.MFI.Models.RequestModels;
using GC.MFI.Models.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GC.MFI.DataAccess.Repository.Implementations
{
    public class PortalSavingSummaryRepository : LegacyRepositoryBase<PortalSavingSummary>, IPortalSavingSummaryRepository
    {
        public PortalSavingSummaryRepository(IDatabaseFactory databaseFactory) : base(databaseFactory)
        {
        }

        public async Task<PortalSavingSummary> Create(PortalSavingSummary request)
        {
            BeginTransaction();
            var model = new PortalSavingSummary()
            {
                OfficeID = request.OfficeID,
                MemberID = request.MemberID,
                ProductID = request.ProductID,
                CenterID = request.CenterID,
                SavingInstallment = request.SavingInstallment,
                CreateDate = DateTime.UtcNow,
                CreateUser = request.CreateUser,
                OpeningDate = DateTime.UtcNow,
                MemberNomines = request.MemberNomines,
                ApprovalStatus = false
            };
            DataContext.Add(model);
            CommitTransaction();
            return model;
        }
        public async Task<PagedResponse<IEnumerable<PortalSavingSummary>>> GetAllPortalSavingSummaryPaged(PaginationFilter<PortalSavingSummary> filter)
        {
            var TotalElement = DataContext.PortalSavingSummary.Count(t => t.ApprovalStatus == true);

            var savingSummary = DataContext.PortalSavingSummary
                                    .Where(filter.search)
                                    .Where(x => x.ApprovalStatus == true)
                                    .Skip(filter.pageNum > 0 ? (filter.pageNum - 1) * filter.pageSize : 0)
                                    .Take(filter.pageSize).ToList();
            for(int i=0;i<savingSummary.Count();i++)
            {
                var nominee = DataContext.NomineeXPortalSavingSummary.Where(t => t.PortalSavingSummaryId == savingSummary[i].PortalSavingSummaryID);
            }

            return new PagedResponse<IEnumerable<PortalSavingSummary>>(
                savingSummary,
                filter.pageNum,
                filter.pageSize,
                TotalElement,
                TotalElement / filter.pageSize);
        }
    }
}