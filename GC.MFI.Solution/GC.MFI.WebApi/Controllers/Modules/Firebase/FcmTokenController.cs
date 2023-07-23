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
    public Task<IActionResult> Save(FcmTokenViewModel model)
    {
        var userId = long.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier));
        if (model.UserId != userId)
        {
            return Task.FromResult<IActionResult>(Forbid("You are not authorized to perform this action"));
        }

        try
        {
            var result = _service.CreateOrUpdate(model);

            return result != null
                ? Task.FromResult<IActionResult>(Ok(result))
                : Task.FromResult<IActionResult>(NotFound());
        }
        catch (Exception e)
        {
            _logger.LogError(e, "Error Occurred");
            return Task.FromResult<IActionResult>(BadRequest(e.Message));
        }
    }

    [HttpGet]
    [Authorize]
    public Task<IActionResult> Get()
    {
        try
        {
            var result = _service.GetByUserId(long.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier)));

            return result != null
                ? Task.FromResult<IActionResult>(Ok(result))
                : Task.FromResult<IActionResult>(NotFound());
        }
        catch (Exception e)
        {
            _logger.LogError(e, "Error Occurred");
            return Task.FromResult<IActionResult>(BadRequest(e.Message));
        }
    }
}