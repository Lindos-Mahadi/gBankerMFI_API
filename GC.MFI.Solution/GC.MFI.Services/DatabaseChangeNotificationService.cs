
using GC.MFI.Models.DbModels;
using GC.MFI.Services.Modules.GcMfi.Interfaces;
using GC.MFI.Utility.Helpers;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.SignalR;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace GC.MFI.Services
{
    public class DatabaseChangeNotificationService :  IHostedService
    {
        private readonly string _connectionString;
        private readonly IHubContext<ChatHub> _hubContext;
        private readonly IMemoryCache memoryCache;
        public DatabaseChangeNotificationService(IConfiguration configuration,  IMemoryCache memoryCache,  IHubContext<ChatHub> hubContext)
        {
            _connectionString = configuration.GetConnectionString("DefaultConnection");
            _hubContext = hubContext;
            this.memoryCache = memoryCache;
        }
        
        public Task StartAsync(CancellationToken cancellationToken)
        {
            SqlDependency.Start(_connectionString);

            using (var connection = new SqlConnection(_connectionString))
            {
                connection.Open();

                using (var command = new SqlCommand(@"SELECT [Id] ,[Message] ,[SenderType] ,[SenderID] ,[ReceiverType],[ReceiverID],[Email]
                                                    ,[SMS] ,[Push] ,[Status] ,[CreateDate] ,[CreateUser] ,[UpdateDate],[UpdateUser] 
                                                    FROM [dbo].[NotificationTable]", connection))
                {
                    command.Notification = null;

                    SqlDependency dependency = new SqlDependency(command);

                    dependency.OnChange += new OnChangeEventHandler(OnDependencyChange);

                    command.ExecuteReader();
                }
            }

            return Task.CompletedTask;
        }
        private void OnDependencyChange(object sender, SqlNotificationEventArgs e)
        {
            if (e.Type == SqlNotificationType.Change)
            {
                using (var connection = new SqlConnection(_connectionString))
                {
                    connection.Open();
                    var token = memoryCache.Get("useridentifier");
                    long memberId = Convert.ToInt64(JwtTokenDecode.GetDetailsFromToken(token.ToString()).MemberID);
                    using (var command = new SqlCommand(@"SELECT [Id] ,[Message] ,[SenderType] ,[SenderID] ,[ReceiverType],[ReceiverID],[Email]
                                                    ,[SMS] ,[Push] ,[Status] ,[CreateDate] ,[CreateUser] ,[UpdateDate],[UpdateUser] 
                                                    FROM [dbo].[NotificationTable] WHERE [Push] = 'True' AND [ReceiverID] = @memberId ", connection))
                    {

                        command.Parameters.AddWithValue("@memberId", memberId);
                        command.Notification = null;
                        SqlDependency dependency = new SqlDependency(command);

                        dependency.OnChange += new OnChangeEventHandler(OnDependencyChange);

                        var Notification = new List<NotificationTable>();

                        using (var reader = command.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                var notify = new NotificationTable
                                {
                                    Id = reader.GetInt64(0),
                                    Message = reader.GetString(1),
                                    SenderType = reader.GetString(2),
                                    SenderID = reader.IsDBNull(3) ? null : (long?)reader.GetInt64(3),
                                    ReceiverType = reader.GetString(4),
                                    ReceiverID = reader.IsDBNull(5) ? null : (long?)reader.GetInt64(5),
                                    Email = reader.GetBoolean(6),
                                    Sms = reader.GetBoolean(7),
                                    Push = reader.GetBoolean(8)
                                };

                                Notification.Add(notify);
                            }
                        }
                        using (var command2 = new SqlCommand(@"SELECT [Id] ,[MemberID] ,[ConnID] 
                                                    FROM [dbo].[SingalRConnectionTable] WHERE [MemberID] = @memberId ", connection))
                        {
                            command2.Parameters.AddWithValue("@memberId", memberId);
                            using (var reader = command2.ExecuteReader())
                            {
                                if (reader.Read())
                                {
                                    // Get the connection ID from the SqlDataReader
                                    string connId = reader.GetString(2);

                                    // Use the connection ID here
                                    // ...

                                    // Send the notification to the client
                                     _hubContext.Clients.Client(connId).SendAsync("Notification", Notification);
                                }
                                else
                                {
                                    // No rows were returned
                                    // Handle the error here
                                }
                            }
                        }

                    }
                }
            }
        }
        public Task StopAsync(CancellationToken cancellationToken)
        {
            SqlDependency.Stop(_connectionString);

            return Task.CompletedTask;
        }
    }
}
