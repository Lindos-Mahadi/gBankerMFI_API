using AutoMapper;
using AutoMapper.Execution;
using GC.MFI.DataAccess.InfrastructureBase;
using GC.MFI.DataAccess.Repository.Interfaces;
using GC.MFI.Models.DbModels;
using GC.MFI.Models.ViewModels;
using GC.MFI.Services.Modules.GcMfi.Interfaces;
using GC.MFI.Services.Modules.Security.Interfaces;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory.Database;

namespace GC.MFI.Services.Modules.GcMfi.Implementations
{
    public class NotificationTableService : ServiceBase<NotificationTableViewModel, NotificationTable>, INotificationTableService
    {
        private readonly INotificationTableRepository _repository;
        private readonly string _connectionString;  
        public NotificationTableService(IConfiguration configuration,INotificationTableRepository repository, IUnitOfWork unitOfWork, IMapper _mapper) : base(repository, unitOfWork, _mapper)
        {
            this._repository = repository;
            _connectionString = configuration.GetConnectionString("DefaultConnection");
        }

        public Task ViewStatus(long MemberId)
        {
            var CheckingViewNotification = _repository.GetMany(t => t.ReceiverID == MemberId && t.Push == true);
            if(CheckingViewNotification.Count() > 0) 
            {
                SqlDependency.Start(_connectionString);
                using (var connection = new SqlConnection(_connectionString))
                {
                    connection.Open();

                    using (var command3 = new SqlCommand(@"UPDATE [dbo].[NotificationTable] 
                                            SET [Push] = 'False'
                                            WHERE [Push] = 'True' AND [ReceiverID] = @memberId ", connection))
                    {
                        command3.Parameters.AddWithValue("@memberId", MemberId);
                        using (var reader2 = command3.ExecuteReader())
                        {
                        }
                    }

                }
            }

            return Task.CompletedTask;
        }
    }
}
