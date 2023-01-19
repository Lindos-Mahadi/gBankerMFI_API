using GC.MFI.Models.DbModels;
using GC.MFI.Services.Modules.GcMfi.Interfaces;
using GC.MFI.Services.Modules.Security.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;

namespace GC.MFI.WebApi.Controllers.Modules.GcMfi
{
    [Route("api/gcmfi/Investor")]
    public class InvestorController : GCMcfinaLegacyBaseController<Investor>
    {
        private readonly ILogger<InvestorController> _logger;
        private readonly IInvestorService _service;

        public InvestorController(ILogger<InvestorController> logger, IInvestorService service) : base(service)
        {
            this._logger = logger;
            this._service = service;
        }

        [HttpGet]
        [Route("GetInvestorByOrgId")]
        public async Task<IEnumerable<Investor>> GetInvestorByOrgId(long Id)
        {
            try
            {
                var record = _service.GetMany(u => u.OrgID == Id && u.IsActive == true).OrderBy(u=> u.InvestorID);
                return record;
            }
            catch (Exception ex)
            {
                LogError(ex, null);
                throw;
            }
        }
    }
}
