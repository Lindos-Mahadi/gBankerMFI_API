﻿using GC.MFI.Models.DbModels;
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

        public DistrictController(ILogger<DistrictController> logger, IDistrictService service) : base(service)
        {
            this._logger = logger;
            this._service = service;
        }

        [HttpGet]
        [Route("getdistrictbydivisionid")]
        public async Task<IEnumerable<District>> GetDistrictByDivisionId(int divisionId)
        {
            var getDistrict =  _service.GetMany(t => t.DivisionID == divisionId).AsEnumerable();
            return getDistrict;

        }
    }
}