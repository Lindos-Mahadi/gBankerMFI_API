using GC.MFI.DataAccess.InfrastructureBase;
using GC.MFI.DataAccess.Repository.Interfaces;
using GC.MFI.Models.DbModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GC.MFI.DataAccess.Repository.Implementations
{
    public class FileUploadRepository : LegacyRepositoryBase<FileUploadTable>, IFileUploadRepository
    {
        public FileUploadRepository(IDatabaseFactory databaseFactory) : base(databaseFactory)
        {
        }

        public async Task<FileUploadTable> CreateFileUpload(FileUploadTable fileCreate)
        {
            BeginTransaction();
            var fCreate = new FileUploadTable()
            {
                EntityName = fileCreate.EntityName,
                EntityId = fileCreate.EntityId,
                PropertyName = fileCreate.PropertyName,
                File = fileCreate.File,
                FileName = fileCreate.FileName,
                Type = fileCreate.Type
            };
            DataContext.Add(fCreate);
            CommitTransaction();
            return fCreate;
            
        }
    }
}

