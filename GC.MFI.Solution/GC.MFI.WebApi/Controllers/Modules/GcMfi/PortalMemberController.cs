using GC.MFI.Models.DbModels;
using GC.MFI.Models.ViewModels;
using GC.MFI.Services.Modules.BntPos.Interfaces;
using GC.MFI.Services.Modules.GcMfi.Interfaces;
using GC.MFI.Utility.Helpers;
using GC.MFI.WebApi.Controllers.Modules.Pos;
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
    [Route("api/gcmfi/portalmember")]
    public class PortalMemberController : GcMfiMembePortalBaseController<PortalMemberViewModel, PortalMember>
    {
        private readonly ILogger<PortalMemberController> _logger;
        private readonly IPortalMemberService _service;


        public PortalMemberController(ILogger<PortalMemberController> logger, IPortalMemberService service) : base(service)
        {
            _logger = logger;
            _service = service;
        }
        [Authorize]
        [HttpGet]
        [Route("portalmemberprofile")]
        public async Task<MemberProfile> GetMemberById()
        {
            try
            {
                var header = AuthenticationHeaderValue.Parse(Request.Headers["Authorization"]).Parameter;
                var tokenInfo = JwtTokenDecode.GetDetailsFromToken(header);
                var member = await _service.GetMemberById(tokenInfo.PortalMemberId);
                return member;
            }
            catch(Exception ex)
            {
                throw ex;
            }
            
        }

        [HttpGet]
        [Route("getEducationList")]
        public async Task<string> getEducationList()
        {
            var jObject = new[]
            {
                new { Text = "Pre-Primary", Value = "1" },
                new  { Text = "Primary", Value = "2" },
                //new { Text = "JSC", Value = "3" },
                new { Text = "Secondary", Value = "3" },
                new { Text = "Higher Secondary", Value = "4" },
                //new { Text = "Diploma", Value = "6" },
                new { Text = "Graduate", Value = "5" },
                new { Text = "PostGraduate", Value = "6" },
                //new { Text = "Illiterate", Value = "9" },
                new { Text = "Other", Value = "7" }
            };

            return JsonConvert.SerializeObject(jObject);
        }

        [HttpGet]
        [Route("getOccupationList")]
        public async Task<string> getOccupationList()
        {
            var jObject = new[]
            {
                new { Text = "SelfEmployed", Value = "SF" },
                new { Text = "Service", Value = "SE" },
                new { Text = "Business", Value = "BU" },
                new { Text = "House Hold", Value = "HH" },
                new { Text = "Farmer", Value = "FR" },
                new { Text = "Agriculture", Value = "AG" },
                new { Text = "Others", Value = "OT" }

            };
            return JsonConvert.SerializeObject(jObject);
        }
        [HttpGet]
        [Route("getHomeTypeList")]
        public async Task<string> getHomeTypeList()
        {
            var jObject = new[]
            {
                new { Text = "Building", Value = "BU" },
                new { Text = "Muddy", Value = "MU" },
                new { Text = "Rented", Value = "RE" },
                new { Text = "Semi Building", Value = "SB" },
                new { Text = "Tin Shade", Value = "TN" }
            };
            return JsonConvert.SerializeObject(jObject);
        }

        [HttpGet]
        [Route("getCitizenshipList")]
        public async Task<string> getCitizenshipList()
        {
            var jObject = new[]
            {
                new { Text = "By Birth", Value = "BB" },
                new { Text = "Migrated", Value = "MI" },
                new { Text = "Marital", Value = "MA" },
                new { Text = "Nutralization", Value = "NU" }
            };
            return JsonConvert.SerializeObject(jObject);
        }
    }
}

