using System.Security.Claims;
using GC.MFI.Models.ViewModels;
using GC.MFI.Services.Modules.Firebase.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace GC.MFI.WebApi.Controllers.Modules.Firebase;

[Route("api/[controller]")]
[ApiController]
public class FcmTokenController : ControllerBase
{
    private readonly ILogger<FcmTokenController> _logger;
    private readonly IFcmTokenService _service;

    public FcmTokenController(ILogger<FcmTokenController> logger,
        IFcmTokenService service)
    {
        _logger = logger;
        _service = service;
    }

    [HttpPost]
    [Authorize]
    public async Task<ActionResult<FcmTokenViewModel>> Save(FcmTokenInputModel input)
    {
       // var userId = User.FindFirstValue("id");

        var model = new FcmTokenViewModel
        {
           
            DeviceId = input.DeviceId,
            IsMobile = input.IsMobile
        };

        try
        {
            var result = await _service.CreateOrUpdate(model);

            return result != null
                ? Ok(result)
                : BadRequest();
        }
        catch (Exception e)
        {
            _logger.LogError(e, "Error Occurred");
            return BadRequest(e.Message);
        }
    }

    [HttpGet]
    [Authorize]
    public async Task<ActionResult<FcmTokenViewModel>> Get()
    {
        try
        {
            var userId = User.FindFirstValue("id");

            var result = await _service.GetByUserId(userId);

            return result != null
                ? Ok(result)
                : NotFound();
        }
        catch (Exception e)
        {
            _logger.LogError(e, "Error Occurred");
            return BadRequest(e.Message);
        }
    }
}