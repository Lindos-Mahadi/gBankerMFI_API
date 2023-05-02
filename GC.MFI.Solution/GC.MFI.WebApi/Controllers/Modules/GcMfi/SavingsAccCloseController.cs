using GC.MFI.Models.DbModels;
using GC.MFI.Models.ViewModels;
using GC.MFI.Services.Modules.BntPos.Interfaces;
using GC.MFI.Services.Modules.GcMfi.Interfaces;
using GC.MFI.Utility.Helpers;
using GC.MFI.WebApi.Controllers.Modules.Pos;
using GC.MFI.WebApi.Filters;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System.Net.Http.Headers;
using System.Text.Json.Nodes;
using static System.Net.Mime.MediaTypeNames;

namespace GC.MFI.WebApi.Controllers.Modules.GcMfi
{
    [Authorize]
    [Route("api/gcmfi/SavingsAccClose")]
    public class SavingsAccCloseController : GcMfiMembePortalBaseController<SavingsAccCloseViewModel, SavingsAccClose>
    {
        private readonly ILogger<ValidationFilterAttribute> _logger;
        private readonly ISavingsAccCloseService _service;
        public SavingsAccCloseController(ILogger<ValidationFilterAttribute> logger, ISavingsAccCloseService service) : base(service)
        {
            _logger = logger;
            _service = service;
        }
        [HttpPost]
        [Route("create")]
        [ServiceFilter(typeof(ValidationFilterAttribute))]
        public override SavingsAccCloseViewModel Create(SavingsAccCloseViewModel acc)
        {
            try
            {
                var header = AuthenticationHeaderValue.Parse(Request.Headers["Authorization"]).Parameter;
                var tokenDecode = JwtTokenDecode.GetDetailsFromToken(header);
                acc.CreateUser = tokenDecode.UserName;
                acc.MemberID = tokenDecode.MemberID;
                acc.OfficeID = long.Parse(tokenDecode.OfficeId);
                var getsavingacc = _service.Create(acc);
                return getsavingacc;
            }
            catch(Exception ex)
            {
                _logger.Log(LogLevel.Error, ex.Message);
                throw;
            }
        }

    }
}

