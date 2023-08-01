using GC.MFI.Models.DbModels;
using GC.MFI.Services.Modules.Firebase.Interfaces;
using GC.MFI.Services.Modules.GcMfi.Interfaces;
using GC.MFI.Services.Modules.Security.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;
using System.Security.Claims;

namespace GC.MFI.WebApi.Controllers.Modules.GcMfi
{
    [Route("api/gcmfi/FcmConnectionTable")]
    public class FcmConnectionTableController : GCMcfinaLegacyBaseController<FcmConnectionTable>
    {
        private readonly ILogger<FcmConnectionTableController> _logger;
        private readonly IFcmConnectionTableService _service;
        private readonly IMemberService memberService;

        public FcmConnectionTableController(ILogger<FcmConnectionTableController> logger, IFcmConnectionTableService service, IMemberService memberService) : base(service)
        {
            this._logger = logger;
            this._service = service;
            this.memberService = memberService;
        }

        [HttpPost]
        [Route("create")]
        //[Authorize]
        public ActionResult<FcmConnectionTable> Create(FcmConnectionTable table)
        {
            var fcm = _service.Get(fcm => fcm.MemberId == table.MemberId);

            if (fcm == null)
            {
                var create = _service.Create(table);
                return Ok(create);
            }

            return BadRequest("You already have an active token! Please delete it first!");

        }


        [HttpGet("getbymemberid/{memberId}")] 
        //[Authorize]
        public ActionResult<FcmConnectionTable> Get(long memberId)
        {
        
            var fcm = _service.Get(fcm => fcm.MemberId == memberId);

            if(fcm == null)
            {
                return NotFound();
            }

            return Ok(fcm);
        }

        [HttpDelete("deletebymemberid/{memberId}")]
        public ActionResult Delete(long memebrId)
        {
            _service.Delete(fcm=>fcm.MemberId == memebrId);

            return NoContent();

        }



    }
}
