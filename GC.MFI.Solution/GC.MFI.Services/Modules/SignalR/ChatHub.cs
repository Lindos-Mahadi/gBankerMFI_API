using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using GC.MFI.Models.DbModels;
using GC.MFI.Services.Modules.GcMfi.Interfaces;
using GC.MFI.Utility.Helpers;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.SignalR;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.Configuration;

public class ChatHub : Hub
{
    private readonly ISignalRConnectionTableService service;
    private readonly INotificationTableService notificationTableService;
    private readonly IMemoryCache memoryCache;
    private readonly IHttpContextAccessor httpContextAccessor;
    public ChatHub(ISignalRConnectionTableService service, IHttpContextAccessor httpContextAccessor, INotificationTableService notificationTableService, IMemoryCache memoryCache)
    {
        this.service = service;
        this.memoryCache = memoryCache;
        this.notificationTableService = notificationTableService;
        this.httpContextAccessor = httpContextAccessor;
    }

    public override async Task OnConnectedAsync()
    {
        var connectionId = Context.ConnectionId;
        var token = httpContextAccessor.HttpContext.Request.Query["access_token"].ToString().Substring(7);
       
        if (token != null)
        {
             var userName = httpContextAccessor.HttpContext.User.Identity.Name;
            var memberId = Convert.ToInt64(JwtTokenDecode.GetDetailsFromToken(token.ToString()).MemberID);
            SignalRConnectionTable singalRConnectionTable = new SignalRConnectionTable
            {
                MemberID = Convert.ToInt64(memberId),
                ConnID = connectionId
            };
            await service.UpdateConnectionTable(singalRConnectionTable);

            var getNotification = notificationTableService.GetMany(t => t.Push == false && t.ReceiverID == memberId).OrderByDescending(t => t.UpdateDate);

            await Clients.Client(connectionId).SendAsync("OLD", getNotification);
            var getnewNotification = notificationTableService.GetMany(t => t.Push == true && t.ReceiverID == memberId).OrderByDescending(t => t.UpdateDate);
            if (getnewNotification.Count() > 0)
            {
                await Clients.Client(connectionId).SendAsync("NEW", getnewNotification);
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