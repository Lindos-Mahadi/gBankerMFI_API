
using GC.MFI.Models.DbModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;
using Polly;

[ApiController]
[Authorize]
[Route("[controller]")]
public class ChatController : ControllerBase
{
    private readonly IHubContext<ChatHub> _hubContext;

    public ChatController(IHubContext<ChatHub> hubContext)
    {
        _hubContext = hubContext;
    }

    [HttpPost]
    [Route("1")]
    public async Task<IActionResult> SendMessage(string user, string message)
    {
      var usern = HttpContext.User.Identity.Name;
      var connectionId = HttpContext.Connection.Id;
       // var user = Context
      await _hubContext.Clients.User(connectionId).SendAsync("ReceiveMessage", user, message);
      return Ok();
    }

    [HttpPost]
    [Route("2")]
    public async Task<IActionResult> SendMessageWithConn(string connID, string message)
    {
        var usern = HttpContext.User.Identity.Name;
        var connectionId = HttpContext.Connection.Id;
        // var user = Context
        await _hubContext.Clients.Client(connID).SendAsync("Notification", new List<NotificationTable>()
        {
            new NotificationTable
            {
                Message = "Hi there"
            }
        });
        await _hubContext.Clients.All.SendAsync("Notification", new List<NotificationTable>());
        return Ok();
    }
}