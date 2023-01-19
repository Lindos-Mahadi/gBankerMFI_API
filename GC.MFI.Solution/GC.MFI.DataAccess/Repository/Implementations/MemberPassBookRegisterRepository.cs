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
    public class MemberPassBookRegisterRepository : LegacyRepositoryBase<MemberPassBookRegister>, IMemberPassBookRegisterRepository
    {
        public MemberPassBookRegisterRepository(IDatabaseFactory databaseFactory) : base(databaseFactory)
        {
        }
    }
}
