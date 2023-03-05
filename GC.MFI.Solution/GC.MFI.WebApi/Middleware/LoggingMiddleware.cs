using GC.MFI.Models.ViewModels;
using GC.MFI.Services.Modules.GcMfi.Interfaces;

namespace GC.MFI.WebApi.Middleware
{
    public class LoggingMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly ILogger _logger;

        public LoggingMiddleware(RequestDelegate next, ILoggerFactory loggerFactory)
        {
            _next = next;
            _logger = loggerFactory.CreateLogger<LoggingMiddleware>();
        }

        public async Task Invoke(HttpContext context, ILoggerService service)
        {
            try
            {
                await _next(context);
            }
            finally
            {
                _logger.LogInformation(
                    "Request {method} {url} => {statusCode}",
                    context.Request?.Method,
                    context.Request?.Path.Value,
                    context.Response?.StatusCode);
                LoggerViewModel log = new LoggerViewModel();

                log.ControllerName = context.Request?.Path.Value;
                log.MethodName = context.Request?.Method;
                log.Request = context.Request?.ToString();
                log.Response = context.Response?.ToString();
                log.EntryLevel = context.Response?.StatusCode > 400 ? "Error" : "Info";
                log.UpdateDate = DateTime.UtcNow;
                log.CreateDate = DateTime.UtcNow;
                service.Create(log);
            }
        }

    }
}
