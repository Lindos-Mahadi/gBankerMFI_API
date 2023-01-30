using GC.MFI.Models;
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
        public async Task<IEnumerable<PortalLoanSummary>> getLoanSummaryStatus(byte type, long portalLoanSummaryID)
        {
            var result = await _service.getByLoanStatus(type, portalLoanSummaryID);
            return result;
        }

    }
}
