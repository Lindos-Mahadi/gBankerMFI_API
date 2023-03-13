using GC.MFI.Models.DbModels;
using GC.MFI.Services.Modules.GcMfi.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Data;

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
    }
}
