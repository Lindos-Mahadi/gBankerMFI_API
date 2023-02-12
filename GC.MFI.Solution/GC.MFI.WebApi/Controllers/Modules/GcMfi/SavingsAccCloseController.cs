using GC.MFI.Models.DbModels;
using GC.MFI.Models.ViewModels;
using GC.MFI.Services.Modules.BntPos.Interfaces;
using GC.MFI.Services.Modules.GcMfi.Interfaces;
using GC.MFI.WebApi.Controllers.Modules.Pos;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System.Text.Json.Nodes;
using static System.Net.Mime.MediaTypeNames;

namespace GC.MFI.WebApi.Controllers.Modules.GcMfi
{
    [Route("api/gcmfi/SavingsAccClose")]
    public class SavingsAccCloseController : GcMfiMembePortalBaseController<SavingsAccCloseViewModel, SavingsAccClose>
    {
        private readonly ILogger<SavingsAccCloseController> _logger;
        private readonly ISavingsAccCloseService _service;
        public SavingsAccCloseController(ILogger<SavingsAccCloseController> logger, ISavingsAccCloseService service) : base(service)
        {
            _logger = logger;
            _service = service;
        }

    }
}

