using GC.MFI.Models.DbModels;
using GC.MFI.Models.ViewModels;
using GC.MFI.Services.Modules.GcMfi.Interfaces;
using GC.MFI.Utility.Helpers;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Net.Http.Headers;

namespace GC.MFI.WebApi.Controllers.Modules.GcMfi
{
    [Authorize]
    [Route("api/gcmfi/LoanAccReschedule")]
    public class LoanAccRescheduleController : GcMfiMembePortalBaseController<LoanAccRescheduleViewModel, LoanAccReschedule>
    {
        private readonly ILogger<LoanAccRescheduleController> _logger;
        private readonly ILoanAccRescheduleService _service;
        public LoanAccRescheduleController(ILogger<LoanAccRescheduleController> logger, ILoanAccRescheduleService service) : base(service)
        {
            _logger = logger;
            _service = service;
        }

        [HttpPost]
        [Route("create")]
        public override LoanAccRescheduleViewModel Create(LoanAccRescheduleViewModel acc)
        {
            try
            {
                var header = AuthenticationHeaderValue.Parse(Request.Headers["Authorization"]).Parameter;
                var decodedToken = JwtTokenDecode.GetDetailsFromToken(header);
                acc.CreateUser = decodedToken.UserName;
                acc.MemberID = long.Parse(decodedToken.MemberID);
                acc.OfficeID = long.Parse(decodedToken.OfficeId);
                var createLoanRes = _service.Create(acc);
                return createLoanRes;
            }
            catch(Exception ex)
            {
                throw ex;
            }
        }

    }
}

