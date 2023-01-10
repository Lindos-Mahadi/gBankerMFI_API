using AutoMapper;
using GC.MFI.DataAccess.InfrastructureBase;
using GC.MFI.Models.DbModels;
using GC.MFI.Models.ViewModels;
using GC.MFI.Services.Modules.Security.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GC.MFI.Services.Modules.Security.Implementations
{
    public class PortalMemberService : ServiceBase<PortalMemberViewModel, PortalMember>, IPortalMemberService
    {
        public PortalMemberService(IRepository<PortalMember> repository, IUnitOfWork unitOfWork, IMapper _mapper) : base(repository, unitOfWork, _mapper)
        {
        }
    }
}
