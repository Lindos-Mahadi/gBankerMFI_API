using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.AspNetCore.Mvc;
using GC.MFI.Services.Modules.GcMfi.Interfaces;
using GC.MFI.Models.ViewModels;

namespace GC.MFI.WebApi.Filters
{
    public class ValidationFilterAttribute : ActionFilterAttribute, IExceptionFilter
    {
        private readonly ILoggerService _service;
        public ValidationFilterAttribute(ILoggerService service)
        {
            this._service = service;
        }

        public override void OnActionExecuting(ActionExecutingContext context)
        {
            if (!context.ModelState.IsValid)
            {
                context.Result = new UnprocessableEntityObjectResult(context.ModelState);
            }

        }

        public override void OnActionExecuted(ActionExecutedContext context) 
        {
            var ActionName = context.ActionDescriptor.DisplayName;
            var ControllerName = context.Controller.GetType().Name;
            LoggerViewModel log = new LoggerViewModel
            {
                ControllerName = ControllerName,
                MethodName = ActionName,
            };
            _service.Create(log);

        }

        public void OnException(ExceptionContext context)
        {

            throw new NotImplementedException();
        }
    }
}
