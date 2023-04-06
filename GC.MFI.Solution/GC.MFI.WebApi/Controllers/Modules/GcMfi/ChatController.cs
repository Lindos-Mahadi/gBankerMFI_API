using Microsoft.AspNet.SignalR;
using Microsoft.AspNetCore.Mvc;

[ApiController]
[Route("[controller]")]
public class ChatController : ControllerBase
{
    private readonly IHubContext<ChatHub> _hubContext;

    public ChatController(IHubContext<ChatHub> hubContext)
    {
        _hubContext = hubContext;
    }

    [HttpPost]
    public async Task<IActionResult> SendMessage(string user, string message)
    {
        await _hubContext.Clients.All.SendMessage( user, message);
        return Ok();
    }
}