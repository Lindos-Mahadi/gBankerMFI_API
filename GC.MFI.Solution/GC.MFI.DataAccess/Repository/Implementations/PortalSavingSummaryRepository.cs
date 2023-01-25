using GC.MFI.DataAccess.InfrastructureBase;
using GC.MFI.DataAccess.Repository.Interfaces;
using GC.MFI.Models.DbModels;
using GC.MFI.Models.RequestModels;
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
                SavingInstallment = request.SavingInstallment,
                CreateDate = DateTime.UtcNow,
                CreateUser = request.CreateUser,
                OpeningDate = DateTime.UtcNow,
                MemberNomines = request.MemberNomines,
                ApprovalStutus = false
            };
            DataContext.Add(model);
            CommitTransaction();
            return model;
        }
    }
}