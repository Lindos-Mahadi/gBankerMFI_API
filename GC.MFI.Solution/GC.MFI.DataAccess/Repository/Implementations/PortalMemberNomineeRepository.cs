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
    public class PortalMemberNomineeRepository : LegacyRepositoryBase<PortalMemberNominee>, IPortalMemberNomineeRepository
    {
        public PortalMemberNomineeRepository(IDatabaseFactory databaseFactory) : base(databaseFactory)
        {
        }
    }
}
