using GC.MFI.Models.DbModels;
using GC.MFI.Models.ViewModels;
using GC.MFI.Services.Modules.BntPos.Interfaces;
using GC.MFI.Services.Modules.GcMfi.Interfaces;
using GC.MFI.WebApi.Controllers.Modules.Pos;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace GC.MFI.WebApi.Controllers.Modules
{
    [Route("api/gcmfi/portalmember")]
    public class PortalMemberController : GcMfiMembePortalBaseController<PortalMemberViewModel, PortalMember>
    {
        private readonly ILogger<PortalMemberController> _logger;
        private readonly IPortalMemberService _service;


        public PortalMemberController(ILogger<PortalMemberController> logger, IPortalMemberService service) : base(service)
        {
            this._logger = logger;
            this._service = service;
        }

        [HttpGet]
        [Route("getmemberbyid")]
        public async Task<MemberProfile> GetMemberById(long Id)
        {
            var member = await _service.GetMemberById(Id);
            return member;
        }
    }
}
