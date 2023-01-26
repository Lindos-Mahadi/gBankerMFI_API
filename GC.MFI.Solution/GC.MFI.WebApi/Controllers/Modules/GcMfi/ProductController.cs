using GC.MFI.Models;
using GC.MFI.Models.DbModels;
using GC.MFI.Models.ViewModels;
using GC.MFI.Services.Modules.GcMfi.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace GC.MFI.WebApi.Controllers.Modules.GcMfi
{
    [Route("api/pos/product")]
    public class ProductController : GCMcfinaLegacyBaseController<Product>
    {
        private readonly ILogger<ProductController> _logger;
        private readonly IProductService _service;

        public ProductController(ILogger<ProductController> logger, IProductService service) : base(service)
        {
            _logger = logger;
            _service = service;
        }

        [HttpGet]
        [Route("getProducById")]
        public override Product GetById(long id)
        {
            var result = _service.GetByIdShort((short)id);
            return result;
        }
    }
}
