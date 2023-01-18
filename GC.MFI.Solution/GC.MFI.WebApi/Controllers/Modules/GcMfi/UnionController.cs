using GC.MFI.Models.DbModels;
using GC.MFI.Services;
using GC.MFI.Services.Modules.GcMfi.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace GC.MFI.WebApi.Controllers.Modules.GcMfi
{
    [Route("api/gcmfi/union")]
    public class UnionController : GCMcfinaLegacyBaseController<Union>
    {
        private readonly ILogger<UnionController> _logger;
        private readonly IUnionService _service;

        public UnionController(ILogger<UnionController> logger, IUnionService service) : base(service)
        {
            this._logger = logger;
            this._service = service;
        }

        [HttpGet]
        [Route("GetAllUnionByName")]
        public async Task<IEnumerable<Union>> GetAllUnionByName(string? search)
        {
            try
            {
                var unionList = await _service.GetAllUnionName(search);
                return unionList;
            }
            catch (Exception ex)
            {
                LogError(ex, null);
                throw;
            }
        }
    }
}
