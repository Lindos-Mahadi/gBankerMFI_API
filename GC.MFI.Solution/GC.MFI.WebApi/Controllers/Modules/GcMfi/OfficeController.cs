using GC.MFI.Models.DbModels;
using GC.MFI.Services.Modules.GcMfi.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace GC.MFI.WebApi.Controllers.Modules.GcMfi
{
    [Route("api/gcmfi/office")]
    [ApiController]
    public class OfficeController : ControllerBase
    {
        private readonly ILogger<OfficeController> _logger;
        private readonly IOfficeService _service;
        public OfficeController(ILogger<OfficeController> logger, IOfficeService service)
        {
            _logger = logger;
            _service = service;
        }

        [HttpGet]
        [Route("getall")]
        public async Task<IEnumerable<Office>> GetAll()
        {
            var officeList = await _service.GetAll();
            return officeList;
        }
    }
}
