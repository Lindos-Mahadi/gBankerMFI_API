using GC.MFI.Models.DbModels;
using GC.MFI.Models.Models;
using GC.MFI.Services.Modules.GcMfi.Implementations;
using GC.MFI.Services.Modules.GcMfi.Interfaces;
using GC.MFI.Services.Modules.Security.Interfaces;
using GC.MFI.WebApi.Filters;
using Microsoft.AspNetCore.Mvc;

namespace GC.MFI.WebApi.Controllers.Modules.GcMfi
{
    [Route("api/gcmfi/FileUpload")]
    public class FileUploadController : GCMcfinaLegacyBaseController<FileUploadTable>
    {
        private readonly ILogger<FileUploadController> _logger;
        private readonly IFileUploadService _service;

        public FileUploadController(ILogger<FileUploadController> logger,
            IFileUploadService service) : base(service)
        {
            this._logger = logger;
            this._service = service;
        }

        [HttpPost]
        [Route("FileCreate")]
        public async Task<FileUploadTable> FileCreate(FileUploadTable fileUploadTable)
        {
            var model = await _service.CreateFileUpload(fileUploadTable);
            return model;
        }
    }
}