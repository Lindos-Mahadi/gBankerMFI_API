using GC.MFI.Models.DbModels;
using GC.MFI.Models.ViewModels;
using GC.MFI.Services.Modules.GcMfi.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace GC.MFI.WebApi.Controllers.Modules.GcMfi
{
    [Route("api/gcmfi/LoanAccClose")]
    public class LoanAccCloseController : GcMfiMembePortalBaseController<LoanAccCloseViewModel, LoanAccClose>
    {
        private readonly ILogger<LoanAccCloseController> _logger;
        private readonly ILoanAccCloseService _service;
        public LoanAccCloseController(ILogger<LoanAccCloseController> logger, ILoanAccCloseService service) : base(service)
        {
            _logger = logger;
            _service = service;
        }

    }
}

