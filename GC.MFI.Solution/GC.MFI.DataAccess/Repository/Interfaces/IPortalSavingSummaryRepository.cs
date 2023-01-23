using GC.MFI.DataAccess.InfrastructureBase;
using GC.MFI.Models.DbModels;
using GC.MFI.Models.RequestModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GC.MFI.DataAccess.Repository.Interfaces
{
    public interface IPortalSavingSummaryRepository : ILegacyRepository<PortalSavingSummary>
    {
        Task<PortalSavingSummary> Create(PortalSavingSummary request);
    }
}
