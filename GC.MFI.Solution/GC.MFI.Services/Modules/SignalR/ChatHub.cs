using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using GC.MFI.Models.DbModels;
using GC.MFI.Services.Modules.GcMfi.Interfaces;
using GC.MFI.Utility.Helpers;
using Microsoft.AspNetCore.SignalR;
using Microsoft.Extensions.Caching.Memory;

public class ChatHub : Hub
{
    private readonly ISignalRConnectionTableService service;
    private readonly IMemoryCache memoryCache;
    public ChatHub(ISignalRConnectionTableService service, IMemoryCache memoryCache)
    {
        this.service = service;
        this.memoryCache = memoryCache;
    }

    public override async Task OnConnectedAsync()
    {
        var connectionId = Context.ConnectionId;

        var token = memoryCache.Get("useridentifier");
        var memberId = Convert.ToInt64(JwtTokenDecode.GetDetailsFromToken(token.ToString()).MemberID);

        SignalRConnectionTable getbyId = service.Get(t => t.MemberID == memberId);
        if (getbyId == null)
        {
            SignalRConnectionTable singalRConnectionTable = new SignalRConnectionTable
            {
                MemberID = Convert.ToInt64(memberId),
                ConnID = connectionId
            };
            service.Create(singalRConnectionTable);
        }
        else
        {
            getbyId.ConnID = connectionId;
            service.Update(getbyId);
        }
        
      //  await Clients.User(connectionId).SendAsync("Notification", "king");

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