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

        public async Task<PortalSavingSummary> Create(SavingAccountModel request)
        {
            BeginTransaction();
            var model = new PortalSavingSummary()
            {
                OfficeID = request.officeId,
                MemberID= request.memberId,
                ProductID = (short)request.productId,
                SavingInstallment = request.savingsInstallment,
                CreateDate= DateTime.UtcNow,
                CreateUser= request.createUser,
                OpeningDate = DateTime.UtcNow
            };
            DataContext.Add(model);
            CommitTransaction();
            return model;
        }
    }
}