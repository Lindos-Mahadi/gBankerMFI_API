using GC.MFI.Models.DbModels;
using GC.MFI.Models.ViewModels;
using GC.MFI.Services.Modules.BntPos.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace GC.MFI.WebApi.Controllers.Modules.Pos
{
    [Route("api/pos/order")]
    public class OrderController : GcMfiMembePortalBaseController<OrderViewModel, Order>
    {
        private readonly ILogger<OrderController> _logger;
        private readonly IOrderService _service;


        public OrderController(ILogger<OrderController> logger, IOrderService service) : base(service)
        {
            this._logger = logger;
            this._service = service;
        }

         

    }
}
