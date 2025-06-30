using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Mvc;

namespace CleanArchitectureDemo.Utils.GlobalExceptionHandler
{
    public class ExceptionHandler : IExceptionHandler
    {
        private readonly ILogger<ExceptionHandler> _logger;

        public ExceptionHandler(ILogger<ExceptionHandler> logger)
        {
            _logger = logger;
        }

        public ValueTask<bool> TryHandleAsync(HttpContext httpContext, Exception exception, CancellationToken cancellationToken)
        {
            ExceptionHandler.HandleException(_logger, httpContext, exception, cancellationToken);
            return ValueTask.FromResult(true);
        }

        public static void HandleException(ILogger logger,HttpContext httpContext, Exception exception, CancellationToken cancellationToken = new CancellationToken())
        {
            var requestId = Guid.NewGuid().ToString();
            var traceId = httpContext.TraceIdentifier;
            var problem = new ProblemDetails
            {
                Type = "https://httpstatuses.com/",
                Instance = httpContext.Request.Path,
                Extensions =
                {
                    ["traceId"] = traceId,
                    ["requestId"] = requestId
                }
            };
            switch (exception)
            {
                case ArgumentException _:
                    problem.Status = StatusCodes.Status400BadRequest;
                    problem.Title = "Bad Request";

                    break;
                case UnauthorizedAccessException _:
                    problem.Status = StatusCodes.Status401Unauthorized;
                    problem.Title = "Unauthorized";
                    problem.Detail = exception.Message;
                    break;
                case KeyNotFoundException _:
                    problem.Status = StatusCodes.Status404NotFound;
                    problem.Title = "Resource Not Found";
                    break;
                default:
                    problem.Status = StatusCodes.Status500InternalServerError;
                    problem.Title = "Internal Server Error";
                    problem.Detail = "An unexpected error occurred. Please try again later.";

                    logger.LogCritical(exception.Message, [("RequestId", requestId), ("TraceId", traceId)]);

                    break;
            }

            httpContext.Response.ContentType = "application/problem+json";
            httpContext.Response.StatusCode = problem.Status.Value;
            httpContext.Response.WriteAsJsonAsync(problem, cancellationToken: cancellationToken);
        }

    }

}
