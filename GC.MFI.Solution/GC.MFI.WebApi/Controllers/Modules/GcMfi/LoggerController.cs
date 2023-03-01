using GC.MFI.Models.DbModels;
using GC.MFI.Models.ViewModels;
using GC.MFI.Services.Modules.BntPos.Interfaces;
using GC.MFI.Services.Modules.GcMfi.Interfaces;
using GC.MFI.Utility.Helpers;
using GC.MFI.WebApi.Controllers.Modules.Pos;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace GC.MFI.WebApi.Controllers.Modules.GcMfi
{
    [Authorize]
    [Route("api/gcmfi/Logger")]
    public class LoggerController : GcMfiMembePortalBaseController<LoggerViewModel, Logger>
    {
        private readonly ILogger<LoggerController> _logger;
        private readonly ILoggerService _service;
        public LoggerController(ILogger<LoggerController> logger, ILoggerService service) : base(service)
        {
            _logger = logger;
            _service = service;
        }
        

    }
}

