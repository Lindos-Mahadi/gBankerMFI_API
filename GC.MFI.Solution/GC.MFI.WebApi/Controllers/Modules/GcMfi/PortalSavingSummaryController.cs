using GC.MFI.Models;
using GC.MFI.Models.DbModels;
using GC.MFI.Models.Modules.Security;
using GC.MFI.Models.RequestModels;
using GC.MFI.Models.ViewModels;
using GC.MFI.Services.Modules.GcMfi.Interfaces;
using GC.MFI.Services.Modules.Security.Interfaces;
using GC.MFI.Utility.Helpers;
using GC.MFI.WebApi.Filters;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;
using System.Data;
using System.Net.Http.Headers;

namespace GC.MFI.WebApi.Controllers.Modules.GcMfi
{
    [Authorize(Roles = "PortalMember, PortalAdmin")]
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

                if (!ModelState.IsValid)
                {
                    var errors = ModelState.Select(x => x.Value.Errors)
                        .Where(y => y.Count > 0)
                        .ToList();
                }
                var header = AuthenticationHeaderValue.Parse(Request.Headers["Authorization"]).Parameter;
                objectToSave.CreateUser = JwtTokenDecode.GetDetailsFromToken(header).UserName;
                objectToSave.CreateDate = DateTime.UtcNow;
                var response = _service.CreatePortalSavingSummary(objectToSave);
                if (response == null)
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
        public async Task<IEnumerable<SavingSummaryViewModel>> getSavingSummaryStatus(byte type , long memberId)
        {
            var result = await _service.getBySavingStatus(type , memberId);
            return result;
        }

        [HttpGet]
        [Route("getSavingSummaryStatusbyMemberId")]
        public async Task<IEnumerable<SavingSummaryViewModel>> getSavingSummaryStatus(long memberId)
        {
            var result = await _service.getBySavingStatus(memberId);
            return result;
        }

        [HttpGet]
        [Route("savingsummarydetails")]
        public async Task<PortalSavingSummaryViewModel> GetPortalSavingSummary(long Id)
        {
            var result = await _service.PortalSavingSummaryDetails(Id);
            return result;
        }

        

    }
}
