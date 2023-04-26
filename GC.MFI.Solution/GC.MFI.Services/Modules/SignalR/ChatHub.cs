using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using GC.MFI.Models.DbModels;
using GC.MFI.Services.Modules.GcMfi.Interfaces;
using GC.MFI.Utility.Helpers;
using Microsoft.AspNetCore.SignalR;
using Microsoft.Extensions.Caching.Memory;

public class ChatHub : Hub
{
    private readonly ISignalRConnectionTableService service;
    private readonly INotificationTableService notificationTableService;
    private readonly IMemoryCache memoryCache;
    public ChatHub(ISignalRConnectionTableService service, INotificationTableService notificationTableService, IMemoryCache memoryCache)
    {
        this.service = service;
        this.memoryCache = memoryCache;
        this.notificationTableService = notificationTableService;
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
        var getNotification = notificationTableService.GetMany(t => t.Push == false && t.ReceiverID == memberId).OrderByDescending(t => t.UpdateDate);
        
        await Clients.Client(connectionId).SendAsync("OLD", getNotification);
        var getnewNotification = notificationTableService.GetMany(t=> t.Push == true && t.ReceiverID == memberId).OrderByDescending(t => t.UpdateDate);
        if(getnewNotification != null) 
        {
            foreach(var notification in getnewNotification)
            {
                notification.Status = "P";
                notificationTableService.Update(notification);
            }
        }
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