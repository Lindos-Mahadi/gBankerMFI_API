using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.AspNetCore.Mvc;

namespace GC.MFI.WebApi.Filters
{
    public class ValidationFilterAttribute : ActionFilterAttribute, IExceptionFilter
    {
        public override void OnActionExecuting(ActionExecutingContext context)
        {
            if (!context.ModelState.IsValid)
            {
                context.Result = new UnprocessableEntityObjectResult(context.ModelState);
            }
        }

        public override void OnActionExecuted(ActionExecutedContext context) { }

        public void OnException(ExceptionContext context)
        {
            throw new NotImplementedException();
        }
    }
}
