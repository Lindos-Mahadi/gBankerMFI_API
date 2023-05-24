using GC.MFI.Models;
using GC.MFI.Models.DbModels;
using GC.MFI.Models.Modules.Security;
using GC.MFI.Models.ViewModels;
using GC.MFI.Services;
using GC.MFI.Services.Modules.GcMfi.Interfaces;
using GC.MFI.Utility.Helpers;
using GC.MFI.WebApi.Filters;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Data;
using System.Net.Http.Headers;

namespace GC.MFI.WebApi.Controllers.Modules.GcMfi
{
    [Authorize(Roles = "PortalMember, PortalAdmin")]
    [Route("api/gcmfi/PortalLoanSummary")]
    public class PortalLoanSummaryController : GCMcfinaLegacyBaseController<PortalLoanSummary>
    {
        private readonly ILogger<PortalLoanSummaryController> _logger;
        private readonly IPortalLoanSummaryService _service;

        public PortalLoanSummaryController(ILogger<PortalLoanSummaryController> logger, IPortalLoanSummaryService service) : base(service)
        {
            this._logger = logger;
            this._service = service;
        }

        [HttpPost]
        [Route("create")]
        [ServiceFilter(typeof(ValidationFilterAttribute))]
        public IActionResult Create([FromBody] PortalLoanSummaryFileUpload objectToSave)
        {
            try
            {

                if (!ModelState.IsValid)
                {
                    var errors = ModelState.Select(x => x.Value.Errors)
                        .Where(y => y.Count > 0)
                        .ToList();
                }
               var header = AuthenticationHeaderValue.Parse(Request.Headers["Authorization"]).Parameter;
               var userName = JwtTokenDecode.GetDetailsFromToken(header).UserName;
               objectToSave.CreateUser = userName;
               objectToSave.CreateDate = DateTime.UtcNow;
                var response = _service.CreatePortalLoanSummary(objectToSave);
                if(response == null)
                {
                    SignUpResponse signUpResponse = new SignUpResponse();
                    signUpResponse.isSuccess = false;
                    signUpResponse.message = "Invalid File";
                    return BadRequest(signUpResponse);
                }
                return Ok(objectToSave);
            }
            catch (Exception ex)
            {
                LogError(ex, null);
                throw;
            }
        }

        [HttpGet]
        [Route("getactiveloansummary")]
        public async Task<IEnumerable<PortalLoanSummary>> GetActiveLoanSummary()
        {
            var result = _service.GetMany(t=> t.IsActive == true);
            return result;
        }

        [HttpGet]
        [Route("pagedloansummary")]
        public async Task<PagedResponse<IEnumerable<PortalLoanSummaryViewModel>>> PagedLoanSummary([FromQuery] PagedFilter filter,long Id)
        {
            var filt = new PaginationFilter<PortalLoanSummaryViewModel>(filter.pageNum, filter.pageSize);
            if (!String.IsNullOrEmpty(filter.search))
            {
               filt = new PaginationFilter<PortalLoanSummaryViewModel>(
                   filter.pageNum,
                   filter.pageSize,
                   t=> t.ProductName.Trim().Replace(" ","").ToUpper()!.Contains(filter.search.Trim().Replace(" ", "").ToUpper()));
            }
            
            var summary = await _service.GetAllPortalLoanSummaryPaged(filt, Id);
            return summary;
        }

        [HttpGet]
        [Route("getLoanSummaryStatus")]
        public async Task<IEnumerable<PortalLoanSummaryViewModel>> getLoanSummaryStatus(byte type, long memberId)
        {
            var result = await _service.getByLoanStatus(type, memberId);
            return result;
        }
        [HttpGet]
        [Route("getloansummary")]
        public virtual PortalLoanSummaryViewModel GetLoanSummary(long Id)
        {
            var getLoan = _service.GetById(Id);
            return getLoan;
        }

    }
}
