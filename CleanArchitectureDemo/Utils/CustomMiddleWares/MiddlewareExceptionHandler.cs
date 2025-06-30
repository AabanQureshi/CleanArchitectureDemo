using CleanArchitectureDemo.Utils.GlobalExceptionHandler;

namespace CleanArchitectureDemo.Utils.CustomMiddleWares
{
    public class MiddlewareExceptionHandler
    {
        private readonly RequestDelegate _next;
        private readonly ILogger<MiddlewareExceptionHandler> _logger;

        public MiddlewareExceptionHandler(RequestDelegate next, ILogger<MiddlewareExceptionHandler> logger)
        {
            _next = next;
            _logger = logger;
        }

        public async Task InvokeAsync(HttpContext context)
        {
            try
            {
                await _next(context);
            }
            catch (Exception ex)
            {
                ExceptionHandler.HandleException(_logger, context, ex);
            }
        }
    }
}
