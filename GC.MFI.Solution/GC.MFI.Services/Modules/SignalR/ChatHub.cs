using System.Collections.Generic;
using System.Threading.Tasks;
using GC.MFI.Models.DbModels;
using Microsoft.AspNetCore.SignalR;

public class ChatHub : Hub
{

    public override async Task OnConnectedAsync()
    {
        var connectionId = Context.ConnectionId;
        await Clients.User(connectionId).SendAsync("Notification", "king");

        await base.OnConnectedAsync();
    }
    public async Task SendMessage(string user, string message)
    {
        await Clients.All.SendAsync("ReceiveMessage", user, message);
    }

    public async Task NotificationUpdated(List<NotificationTable> Notification)
    {
        await Clients.User(Context.User.Identity.Name).SendAsync("Notification", Notification);
    }

}