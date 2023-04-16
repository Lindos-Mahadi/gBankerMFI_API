using System.Collections.Generic;
using System.Threading.Tasks;
using GC.MFI.Models.DbModels;
using Microsoft.AspNetCore.SignalR;

public class ChatHub : Hub
{
    public async Task SendMessage(string user, string message)
    {
        await Clients.All.SendAsync("ReceiveMessage", user, message);
    }

    public async Task NotificationUpdated(List<NotificationTable> Notification)
    {
        await Clients.User(Context.ConnectionId).SendAsync("Notification", Notification);
    }

}