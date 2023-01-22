using GC.MFI.Models.DbModels;
using GC.MFI.Services;
using GC.MFI.Services.Modules.GcMfi.Interfaces;
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
        public virtual PortalLoanSummary Create(PortalLoanSummary objectToSave)
        {
            try
            {

                objectToSave.CreateUser = "Administrator";
                objectToSave.CreateDate = DateTime.UtcNow;
                _service.Create(objectToSave);
                return objectToSave;
            }
            catch (Exception ex)
            {
                LogError(ex, null);
                throw;
            }
        }
    }
}
