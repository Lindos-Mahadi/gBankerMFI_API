﻿using GC.MFI.DataAccess.InfrastructureBase;
using GC.MFI.Models.DbModels;
using System.Threading.Tasks;

namespace GC.MFI.DataAccess.Repository.Interfaces
{
    public interface IPortalMemberRepository : IRepository<PortalMember>
    {
        Task<PortalMember> CreatePortalMember(SignUpModel signUp);
    }
}
