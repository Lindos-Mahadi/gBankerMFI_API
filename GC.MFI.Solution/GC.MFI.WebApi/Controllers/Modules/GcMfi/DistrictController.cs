using GC.MFI.Models.DbModels;
using GC.MFI.Models.Models;
using GC.MFI.Services.Modules.GcMfi.Implementations;
using GC.MFI.Services.Modules.GcMfi.Interfaces;
using GC.MFI.Services.Modules.Security.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace GC.MFI.WebApi.Controllers.Modules.GcMfi
{
    [Route("api/gcmfi/district")]
    public class DistrictController : GCMcfinaLegacyBaseController<District>
    {
        private readonly ILogger<DistrictController> _logger;
        private readonly IDistrictService _service;
        private readonly IStoredProcedureService _storedProcedureService;

        public DistrictController(
            ILogger<DistrictController> logger, 
            IDistrictService service,
            IStoredProcedureService storedProcedureService) : base(service)
        {
            this._logger = logger;
            this._service = service;
            _storedProcedureService = storedProcedureService;
        }
        [HttpGet]
        [Route("getall")]
        public override IEnumerable<District> GetAll()
        {
            var district = _service.GetAll().OrderBy(t=> t.DistrictName);
            return district;
        }
        [HttpGet]
        [Route("getdistrictbydivisionid")]
        public async Task<IEnumerable<DistrictList>> GetDistrictByDivisionId(string divisionId)
        {
            var districts = await _storedProcedureService.GetDistrictByDivision(divisionId);
            return districts;
        }
        [HttpGet]
        [Route("GetAllDistricts")]
        public async  Task<IEnumerable<DistrictList>> GetAllDistricts()
        {
            return await _storedProcedureService.GetAllDistrict();
        }
    }
}