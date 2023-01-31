using GC.MFI.Models.DbModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GC.MFI.Services.Modules.GcMfi.Interfaces
{
    public interface IFileUploadService : ILegacyServiceBase<FileUploadTable>
    {
        Task<FileUploadTable> CreateFileUpload(FileUploadTable fileCreate);
    }
}