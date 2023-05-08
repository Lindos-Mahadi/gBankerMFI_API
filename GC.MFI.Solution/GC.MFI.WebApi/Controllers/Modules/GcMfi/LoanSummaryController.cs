using GC.MFI.DataAccess;
using GC.MFI.Models;
using GC.MFI.Models.DbModels;
using GC.MFI.Models.ViewModels;
using GC.MFI.Services.Modules.GcMfi.Interfaces;
using GC.MFI.Utility.Helpers;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Data;
using System.Net.Http.Headers;

namespace GC.MFI.WebApi.Controllers.Modules.GcMfi
{
    [Authorize(Roles = "PortalMember, PortalAdmin")]
    [Route("api/gcmfi/LoanSummary")]
    public class LoanSummaryController :  GCMcfinaLegacyBaseController<LoanSummary>
    {
        private readonly ILogger<LoanSummaryController> _logger;
        private readonly ILoanSummaryService _service;

        public LoanSummaryController(ILogger<LoanSummaryController> logger, ILoanSummaryService service) : base(service)
        {
            this._logger = logger;
            this._service = service;
        }
        [Route("getloansummary")]
        [HttpGet]
        public IActionResult GetLoanSummary([FromQuery] PagedFilter filter)
        {
            var header = AuthenticationHeaderValue.Parse(Request.Headers["Authorization"]).Parameter;
            var info = JwtTokenDecode.GetDetailsFromToken(header);
            var filt = new PaginationFilter<LoanSummaryViewModel>(filter.pageNum, filter.pageSize);
            if (!String.IsNullOrEmpty(filter.search))
            {
                filt = new PaginationFilter<LoanSummaryViewModel>(
                    filter.pageNum,
                    filter.pageSize,
                    t => t.ProductName.Trim().Replace(" ", "").ToUpper()!.Contains(filter.search.Trim().Replace(" ", "").ToUpper()));
            }
            var response = _service.GetAllPortalLoanSummaryPaged(filt, long.Parse(info.MemberID));
            return Ok(response);
        }
    }
}
