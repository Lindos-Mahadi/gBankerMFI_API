﻿using GC.MFI.Models;
using GC.MFI.Models.DbModels;
using GC.MFI.Models.ViewModels;
using GC.MFI.Services;
using GC.MFI.Services.Modules.GcMfi.Interfaces;
using GC.MFI.WebApi.Filters;
using Microsoft.AspNetCore.Mvc;

namespace GC.MFI.WebApi.Controllers.Modules.GcMfi
{
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
        public IActionResult Create([FromBody] PortalLoanSummary objectToSave)
        {
            try
            {
                //objectToSave.CreateUser = "Administrator";
                //objectToSave.CreateDate = DateTime.UtcNow;
                //_service.Create(objectToSave);
                //return objectToSave;

                if (ModelState.IsValid)
                {
                    objectToSave.CreateUser = "Administrator";
                    objectToSave.CreateDate = DateTime.UtcNow;
                    _service.Create(objectToSave);
                    
                }
                return Ok(objectToSave);
            }
            catch (Exception ex)
            {
                LogError(ex, null);
                throw;
            }
        }

        //[HttpGet]
        //[Route("GetAllPortalSummaries")]
        //public async Task<IActionResult> GetAllPortalSummaries([FromQuery] PaginationFilter<PortalLoanSummary> filter)
        //{
        //    var portalSummaryList = _service.GetAllPortalLoanSummary();
        //    var loanSummaryCount = portalSummaryList.Count();
        //    //if (!String.IsNullOrEmpty(filter.search))
        //    //{
        //    //    //portalSummaryList = portalSummaryList.Where(t => t.CategoryName.ToUpper()!.Contains(filter.search.ToUpper()));
        //    //    //categoryCount = portalSummaryList.Count();

        //    //    // UPDATED WILL BE NEXT
        //    //    portalSummaryList = portalSummaryList.Where(t => t.BankName.ToUpper()!.Contains(filter.search.ToUpper()));
        //    //    loanSummaryCount = portalSummaryList.Count();
        //    //}

        //    //if (filter.per_page > 0)
        //    //{
        //    //    portalSummaryList = portalSummaryList.Skip((filter.page - 1) * filter.per_page).Take(filter.per_page);
        //    //    var toalPage = Convert.ToInt32(Math.Ceiling(((double)loanSummaryCount / (double)filter.per_page)));
        //    //    return Ok(new PagedResponse<IEnumerable<PortalLoanSummary>>(portalSummaryList, filter.page, filter.per_page, loanSummaryCount, toalPage));
        //    //}
        //    //else
        //    //{
        //    //    var toalPage = Convert.ToInt32(Math.Ceiling(((double)loanSummaryCount / (double)loanSummaryCount)));
        //    //    return Ok(new PagedResponse<IEnumerable<PortalLoanSummary>>(portalSummaryList, filter.page, loanSummaryCount, loanSummaryCount, toalPage));
        //    //}
        //}

        [HttpGet]
        [Route("getactiveloansummary")]
        public async Task<IEnumerable<PortalLoanSummary>> GetActiveLoanSummary()
        {
            var result = _service.GetMany(t=> t.IsActive == true);
            return result;
        }

        [HttpGet]
        [Route("pagedloansummary")]
        public async Task<PagedResponse<IEnumerable<PortalLoanSummary>>> PagedLoanSummary([FromQuery] PagedFilter filter)
        {
            var filt = new PaginationFilter<PortalLoanSummary>(filter.pageNum, filter.pageSize);
            var summary = await _service.GetAllPortalLoanSummaryPaged(filt);
            return summary;
        }

    }
}
