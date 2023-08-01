
using FirebaseAdmin.Messaging;
using GC.MFI.Models.DbModels;
using GC.MFI.Models.ViewModels;
using GC.MFI.Services.Modules.Firebase.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.SignalR;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Twilio.Jwt.AccessToken;

namespace GC.MFI.Services
{
    public class DatabaseChangeNotificationService :  IHostedService
    {
        private readonly string _connectionString;
        private readonly IHubContext<ChatHub> _hubContext;
        private readonly IFirebaseNotificationService firebaseNotificationService;
  

        public DatabaseChangeNotificationService(IConfiguration configuration, IHubContext<ChatHub> hubContext, IFirebaseNotificationService firebaseNotificationService)
        {
            _connectionString = configuration.GetConnectionString("DefaultConnection");
            _hubContext = hubContext;
            this.firebaseNotificationService = firebaseNotificationService;

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
        private  async void OnDependencyChange(object sender, SqlNotificationEventArgs e)
        {
            if (e.Type == SqlNotificationType.Change)
            {
               
                 using (var connection = new SqlConnection(_connectionString))
                {
                    connection.Open();

                    //Fire base notification sql start
                    using (var fireBase = new SqlCommand(@"SELECT N.[Id],  N.[Message],  N.[SenderType], N.[SenderID], N.[ReceiverType],  N.[ReceiverID], N.[Email], N.[SMS], N.[Push],
                                                         N.[Status], N.[CreateDate], N.[CreateUser], N.[UpdateDate], N.[UpdateUser]  FROM [dbo].[NotificationTable] N
                                                         WHERE N.[Status] = 'P'", connection))
                    {
                        fireBase.Notification = null;
                        SqlDependency dependency = new SqlDependency(fireBase);

                        dependency.OnChange += new OnChangeEventHandler(OnDependencyChange);
                        var notificationList = new List<NotificationTable>(); // after change you can see the notification list

                        using (var reader = fireBase.ExecuteReader())
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
                                    Push = reader.GetBoolean(8),
                                    Status = reader.GetString(9),
                                };

                                notificationList.Add(notify);
                            }

                        }

                        if(notificationList.Count > 0)
                        {
                            foreach(var noti in notificationList)
                            {

                               


                                using (var fcmToken = new SqlCommand(@"SELECT N.[Token]  FROM [dbo].[FCMConnectionTable] N
                                                         WHERE N.[MemberId] = " + noti.ReceiverID, connection)) 
                                
                                {

                                    using (var readerFCM = fcmToken.ExecuteReader())
                                    {
                                        while (readerFCM.Read())
                                        {
                                            var token = readerFCM.GetString(0);


                                            // Construct the message payload
                                            var message = new Message()
                                            {
                                                Notification = new Notification
                                                {
                                                    Title = noti.ReceiverType,
                                                    Body = noti.Message

                                                },
                                               //Token = $"/topics/{noti.ReceiverID}" //"/topics/all"
                                                Token = "covBsxz_S2yQILcXlGEKV_:APA91bEXCLHaQlTlRROa1lFEiXfjUys-ywC6KDlw2TTWIOvdsH85IO33xjJnYFVeys0sQnWV1rl4iUKZSn5iddi8RaVV_vUm2UFDiAcXSj76qO17vnmiU-GiKN3sJGtCavTOQ5uWG-W7"

                                            };

                                            var result = await firebaseNotificationService.SendAsync(message);

                                        }

                                    }


                                }


                         
                            }
                        }
                    }

                    // fire base sql end

                    using (var command = new SqlCommand(@"SELECT N.[Id],  N.[Message],  N.[SenderType], N.[SenderID], N.[ReceiverType],  N.[ReceiverID], N.[Email], N.[SMS], N.[Push],
                                                         N.[Status], S.[ConnID], N.[CreateDate], N.[CreateUser], N.[UpdateDate], N.[UpdateUser]  FROM [dbo].[NotificationTable] N
	                                                    INNER JOIN [dbo].[SignalRConnectionTable] S
	                                                    ON N.[ReceiverID] = S.[MemberID]
                                                         WHERE N.[Status] = 'P'", connection))
                    {
                        command.Notification = null;
                        SqlDependency dependency = new SqlDependency(command);

                        dependency.OnChange += new OnChangeEventHandler(OnDependencyChange);

                        var Notification = new List<GetNotificationByStatus>();

                        using (var reader = command.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                var notify = new GetNotificationByStatus
                                {
                                    Id = reader.GetInt64(0),
                                    Message = reader.GetString(1),
                                    SenderType = reader.GetString(2),
                                    SenderID = reader.IsDBNull(3) ? null : (long?)reader.GetInt64(3),
                                    ReceiverType = reader.GetString(4),
                                    ReceiverID = reader.IsDBNull(5) ? null : (long?)reader.GetInt64(5),
                                    Email = reader.GetBoolean(6),
                                    Sms = reader.GetBoolean(7),
                                    Push = reader.GetBoolean(8),
                                    Status = reader.GetString(9),
                                    ConnID = reader.GetString(10)
                                };

                                Notification.Add(notify);
                            }
                        }
                            if (Notification.Count > 0)
                            {
                            foreach(var n in Notification)
                            {
                                _hubContext.Clients.Client(n.ConnID).SendAsync("INSTANT", n);
                            }
                            string idList = string.Join(",", Notification.Select(x => x.Id));
                            using (var command2 = new SqlCommand(@"Update [dbo].[NotificationTable] SET [Status]='A' WHERE [Id] IN (" + idList + ")  ", connection))
                            {
                                using (var reader = command2.ExecuteReader())
                                {

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
