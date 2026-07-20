using Microsoft.AspNetCore.Diagnostics;

namespace WebApiTaskTracker.Infrastructure
{
    public class GlobalExceptionHandler : IExceptionHandler
    {
        public async ValueTask<bool> TryHandleAsync(HttpContext httpContext, Exception exception, CancellationToken cancellationToken)
        {
            if (exception is BadHttpRequestException)
            {
                httpContext.Response.StatusCode = StatusCodes.Status400BadRequest;
                await httpContext.Response.WriteAsJsonAsync(new
                {
                    error = "Invalid request content. Ensure body is not empty and JSON is valid."
                }, cancellationToken);

                return true; // Ошибка обработана
            }
            return false;
        }
    }
}
