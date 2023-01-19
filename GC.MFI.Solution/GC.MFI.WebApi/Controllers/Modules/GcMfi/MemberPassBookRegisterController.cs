using GC.MFI.Models.DbModels;
using GC.MFI.Services.Modules.GcMfi.Interfaces;
using GC.MFI.Services.Modules.Security.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;

namespace GC.MFI.WebApi.Controllers.Modules.GcMfi
{
    [Route("api/gcmfi/MemberPassBookRegister")]
    public class MemberPassBookRegisterController : GCMcfinaLegacyBaseController<MemberPassBookRegister>
    {
        private readonly ILogger<MemberPassBookRegisterController> _logger;
        private readonly IMemberPassBookRegisterService _service;

        public MemberPassBookRegisterController(ILogger<MemberPassBookRegisterController> logger, IMemberPassBookRegisterService service) : base(service)
        {
            this._logger = logger;
            this._service = service;
        }

        [HttpGet]
        [Route("LoggedInOrganizationID")]
        public async Task<IEnumerable<MemberPassBookRegister>> GetByOrgId(long orgId)
        {
            var list = _service.GetMany(t=> t.IsActive == true && t.OrgID == orgId).OrderBy(t=> t.MemberPassBookNO);
            return list;
        }

        
    }
}

