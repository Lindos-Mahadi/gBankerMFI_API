using GC.MFI.DataAccess.InfrastructureBase;
using GC.MFI.Models.DbModels;
using GC.MFI.Models.ViewModels;
using GC.MFI.Services;
using GC.MFI.Services.Modules.GcMfi.Implementations;
using GC.MFI.Services.Modules.GcMfi.Interfaces;
using GC.MFI.Utility.Helpers;
using Microsoft.AspNetCore.Mvc;
using System.Net.Http.Headers;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace GC.MFI.WebApi.Controllers.Modules.GcMfi
{
    [Route("api/gcmfi/member")]
    [ApiController]
    public class MemberController : GCMcfinaLegacyBaseController<Member>
    {
        private readonly ILogger<MemberController> _logger;
        private readonly IMemberService _service;
        private readonly IFileUploadService _uploadService;
        public MemberController(ILogger<MemberController> logger, IFileUploadService uploadService, IMemberService service) : base(service)
        {
            _logger = logger;
            _service = service;
            _uploadService = uploadService;
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

        [HttpGet]
        [Route("memberimagebytearray")]
        public async Task<byte[]> GetImageByMemberIDByteArray()
        {
            try
            {
                var header = AuthenticationHeaderValue.Parse(Request.Headers["Authorization"]).Parameter;
                var tokenInfo = JwtTokenDecode.GetDetailsFromToken(header);
                var getMember = _service.GetById(tokenInfo.MemberID);
                if (getMember.Image != null)
                {
                    var image = _uploadService.GetById((long)getMember.Image);
                    return image.File;
                }
                return null;

            }
            catch (Exception ex)
            {
                LogError(ex, null);
                throw;
            }
        }
    }
}
