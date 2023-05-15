using GC.MFI.DataAccess.InfrastructureBase;
using GC.MFI.Models.DbModels;
using GC.MFI.Models.ViewModels;
using GC.MFI.Services;
using GC.MFI.Services.Modules.GcMfi.Interfaces;
using GC.MFI.Utility.Helpers;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Net.Http.Headers;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace GC.MFI.WebApi.Controllers.Modules.GcMfi
{
    [Authorize]
    [Route("api/gcmfi/member")]
    [ApiController]
    public class MemberController : GCMcfinaLegacyBaseController<Member>
    {
        private readonly ILogger<MemberController> _logger;
        private readonly IMemberService _service;
        public MemberController(ILogger<MemberController> logger, IMemberService service) : base(service)
        {
            _logger = logger;
            _service = service;
        }

        [HttpGet]
        [Route("GetMemberByFirstName")]
        public async Task<IEnumerable<Member>> GetMemberByFirstName(string? search)
        {
            try
            {
                var memberList = await _service.GetAllMember(search);
                return memberList;
            }
            catch (Exception ex)
            {
                LogError(ex, null);
                throw;
            }
        }

        [HttpPatch]
        [Route("UpdateMemberProfile")]
        public async Task<Member> UpdateMemberProfile(MemberProfileUpdate member)
        {
            try
            {
                var updateMember = await _service.UpdateMemberProfile(member);
                return updateMember;
            }
            catch (Exception ex)
            {
                LogError(ex, null);
                throw;
            }
        }

        [HttpGet]
        [Route("memberimage")]
        public async Task<string> GetImageByMemberID(long memberId)
        {
            try
            {
                return await _service.GetImageByMemberID(memberId);

            }catch (Exception ex)
            {
                LogError(ex, null);
                throw;
            }
        }

        [HttpPost]
        [Route("updateimage")]
        public async Task<string> UpdateMemberImage(string image)
        {
            try
            {
                var header = AuthenticationHeaderValue.Parse(Request.Headers["Authorization"]).Parameter;
                var tokenInfo = JwtTokenDecode.GetDetailsFromToken(header);
                return await _service.UpdateMemberImage(image,long.Parse(tokenInfo.MemberID));

            }
            catch (Exception ex)
            {
                LogError(ex, null);
                throw;
            }
        }
    }
}
