using GC.MFI.Models;
using GC.MFI.Models.DbModels;
using GC.MFI.Models.RequestModels;
using GC.MFI.Models.ViewModels;
using GC.MFI.Services.Modules.GcMfi.Interfaces;
using GC.MFI.Services.Modules.Security.Interfaces;
using GC.MFI.WebApi.Filters;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;

namespace GC.MFI.WebApi.Controllers.Modules.GcMfi
{
    [Route("api/gcmfi/PortalSavingSummary")]
    public class PortalSavingSummaryController : GCMcfinaLegacyBaseController<PortalSavingSummary>
    {
        private readonly ILogger<PortalSavingSummaryController> _logger;
        private readonly IPortalSavingSummaryService _service;
        private readonly IStoredProcedureService _sp;
        private readonly INomineeXPortalSavingSummaryService _nService; 

        public PortalSavingSummaryController(ILogger<PortalSavingSummaryController> logger, INomineeXPortalSavingSummaryService _nService,IStoredProcedureService sp,  IPortalSavingSummaryService service) : base(service)
        {
            this._logger = logger;
            this._service = service;
            this._sp = sp;
            this._nService = _nService;
        }

        [HttpPost]
        [Route("create")]
        [ServiceFilter(typeof(ValidationFilterAttribute))]
        public IActionResult PortalSavingSummaryFileUpload([FromBody] PortalSavingSummaryFileUpload objectToSave)
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
                    _service.CreatePortalSavingSummary(objectToSave);
                }
                return Ok(objectToSave);
            }
            catch (Exception ex)
            {
                LogError(ex, null);
                throw;
            }
        }

        //[HttpPost]
        //[Route("create")]
        //[ServiceFilter(typeof(ValidationFilterAttribute))]
        //public async Task<PortalSavingSummary> Create(PortalSavingSummary request)
        //{
        //    var model = await _service.Create(request);
        //    return model;
        //}

        [HttpGet]
        [Route("getproductlistforsavingaccount")]
        public async Task<List<ProductListForSavingSummary>> GetList(int orgId,int officeId)
        {
            var result = await _sp.GetProductListForSavingAccount(0, orgId, "S", officeId);
            return result;
        }
        [HttpGet]
        [Route("getallsavingsummary")]
        public async Task<IEnumerable<PortalSavingSummary>> GetSummaryList()
        {
           // var result = _nService.GetMany(t=> t.);
            var getGet =  _service.GetMany(t => t.ApprovalStatus == true).ToList();
            for(int i = 0; i< getGet.Count(); i++ )
            {
                var nominee = _nService.GetMany(t => t.PortalSavingSummaryID == getGet[i].PortalSavingSummaryID);
            }
            return getGet;
        }
        [HttpGet]
        [Route("getapprovesavingsummary")]
        public async Task<PagedResponse<IEnumerable<SavingSummaryViewModel>>> GetAllPortalSavingSummaryPaged([FromQuery] PagedFilter filter, long Id)
        {
            var filt = new PaginationFilter<SavingSummaryViewModel>(filter.pageNum, filter.pageSize);
            
            if(!String.IsNullOrEmpty(filter.search))
            {
                 filt = new PaginationFilter<SavingSummaryViewModel>(filter.pageNum, filter.pageSize, t => t.ProductName.Trim().Replace(" ", "").ToUpper()!.Contains(filter.search.Trim().Replace(" ", "").ToUpper()));
            }
            
            var savingSummary = await _service.GetAllPortalSavingSummaryPaged(filt, Id);
            for (int i = 0; i < savingSummary.Data.Count(); i++)
            {
                var nominee = _nService.GetMany(t => t.PortalSavingSummaryID == savingSummary.Data.ToList()[i].PortalSavingSummaryID);
            }
            return savingSummary;
        }

        [HttpGet]
        [Route("getSavingSummaryStatus")]
        public async Task<IEnumerable<PortalSavingSummary>> getSavingSummaryStatus(byte type , long memberId)
        {
            var result = await _service.getBySavingStatus(type , memberId);
            return result;
        }

        

    }
}
