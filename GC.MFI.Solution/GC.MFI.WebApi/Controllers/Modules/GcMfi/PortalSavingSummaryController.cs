using GC.MFI.Models.DbModels;
using GC.MFI.Models.RequestModels;
using GC.MFI.Services.Modules.GcMfi.Interfaces;
using GC.MFI.Services.Modules.Security.Interfaces;
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

        public PortalSavingSummaryController(ILogger<PortalSavingSummaryController> logger,IStoredProcedureService sp,  IPortalSavingSummaryService service) : base(service)
        {
            this._logger = logger;
            this._service = service;
            this._sp = sp;
        }


        [HttpPost]
        [Route("create")]
        public async Task<PortalSavingSummary> Create(PortalSavingSummary request)
        {
            var model = await _service.Create(request);
            return model;
        }

        [HttpGet]
        [Route("getproductlistforsavingaccount")]
        public async Task<List<ProductList>> GetList(int orgId,int officeId)
        {
            var result = await _sp.GetProductListForSavingAccount(0, orgId, "S", officeId);
            return result;
        }


    }
}
