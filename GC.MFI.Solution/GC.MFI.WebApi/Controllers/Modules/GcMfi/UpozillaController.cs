using GC.MFI.Models.DbModels;
using GC.MFI.Models.Models;
using GC.MFI.Services.Modules.GcMfi.Interfaces;
using GC.MFI.Services.Modules.Security.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;

namespace GC.MFI.WebApi.Controllers.Modules.GcMfi
{
    [Route("api/gcmfi/upozilla")]
    public class UpozillaController : GCMcfinaLegacyBaseController<Upozilla>
    {
        private readonly ILogger<UpozillaController> _logger;
        private readonly IUpozillaService _service;
        private readonly IStoredProcedureService _storedProcedureService;

        public UpozillaController(
            ILogger<UpozillaController> logger, 
            IUpozillaService service,
            IStoredProcedureService storedProcedureService) : base(service)
        {
            this._logger = logger;
            this._service = service;
            _storedProcedureService = storedProcedureService;   
        }

        [HttpGet]
        [Route("GetByDistrictId")]
        public async Task<IEnumerable<UpozillaList>> GetByDistrictId(string Id)
        {
            try
            {
                var record = await _storedProcedureService.GetUpozillaByDistrict(Id);
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
