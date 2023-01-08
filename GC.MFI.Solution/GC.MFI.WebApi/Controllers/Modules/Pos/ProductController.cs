using GC.MFI.Models;
using GC.MFI.Models.DbModels;
using GC.MFI.Models.ViewModels;
using GC.MFI.Services.Modules.BntPos.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace GC.MFI.WebApi.Controllers.Modules.Pos
{
    [Route("api/pos/product")]
    public class ProductController : GcMfiMembePortalBaseController<ProductViewModel, Product>
    {
        private readonly ILogger<ProductController> _logger;
        private readonly IProductService _service;

        public ProductController(ILogger<ProductController> logger, IProductService service) : base(service)
        {
            this._logger = logger;
            this._service = service;
        }
    }
}
