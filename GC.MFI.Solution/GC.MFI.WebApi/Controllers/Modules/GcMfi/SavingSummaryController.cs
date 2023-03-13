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
    [Route("api/gcmfi/SavingSummary")]
    public class SavingSummaryController : GCMcfinaLegacyBaseController<SavingSummary>
    {
        private readonly ILogger<SavingSummaryController> _logger;
        private readonly ISavingSummaryService _service;

        public SavingSummaryController(ILogger<SavingSummaryController> logger, ISavingSummaryService service) : base(service)
        {
            this._logger = logger;
            this._service = service;
        }

        [HttpGet]
        [Route("getapprovesavingsummary")]
        public PagedResponse<IQueryable<SavingsSummaryViewModel>> GetAllPortalSavingSummaryPaged([FromQuery] PagedFilter filter)
        {
            var header = AuthenticationHeaderValue.Parse(Request.Headers["Authorization"]).Parameter;
            var info = JwtTokenDecode.GetDetailsFromToken(header);
            var filt = new PaginationFilter<SavingsSummaryViewModel>(filter.pageNum, filter.pageSize);

            if (!String.IsNullOrEmpty(filter.search))
            {
                filt = new PaginationFilter<SavingsSummaryViewModel>(filter.pageNum, filter.pageSize, t => t.ProductName.Trim().Replace(" ", "").ToUpper()!.Contains(filter.search.Trim().Replace(" ", "").ToUpper()));
            }

            var savingSummary =  _service.GetAllPortalSavingSummaryPaged(filt, long.Parse(info.MemberID));
            //for (int i = 0; i < savingSummary.Data.Count(); i++)
            //{
            //    var nominee = _nService.GetMany(t => t.PortalSavingSummaryID == savingSummary.Data.ToList()[i].PortalSavingSummaryID);
            //}
            return savingSummary;
        }
    }
}
