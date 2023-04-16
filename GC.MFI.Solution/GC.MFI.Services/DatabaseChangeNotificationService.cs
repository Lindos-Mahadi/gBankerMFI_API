
using GC.MFI.Models.DbModels;
using Microsoft.AspNetCore.SignalR;
using Microsoft.Data.SqlClient;
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
    public class DatabaseChangeNotificationService : IHostedService
    {
        private readonly string _connectionString;
        private readonly IHubContext<ChatHub> _hubContext;
        public DatabaseChangeNotificationService(IConfiguration configuration, IHubContext<ChatHub> hubContext)
        {
            _connectionString = configuration.GetConnectionString("DefaultConnection");
            _hubContext = hubContext;
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

                    using (var command = new SqlCommand(@"SELECT [Id] ,[Message] ,[SenderType] ,[SenderID] ,[ReceiverType],[ReceiverID],[Email]
                                                    ,[SMS] ,[Push] ,[Status] ,[CreateDate] ,[CreateUser] ,[UpdateDate],[UpdateUser] 
                                                    FROM [dbo].[NotificationTable]", connection))
                    {
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

                        _hubContext.Clients.All.SendAsync("Notification", Notification);
                    }
                }
               // _hubContext.Clients.All.SendAsync("ReceiveMessage", "Database change notification received.");
            }
        }
        public Task StopAsync(CancellationToken cancellationToken)
        {
            SqlDependency.Stop(_connectionString);

            return Task.CompletedTask;
        }
    }
}
