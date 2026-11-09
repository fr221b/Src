using Microsoft.AspNetCore.Mvc;

namespace OrderService.API.Middleware
{
    public class ExceptionHandlingMiddleware(RequestDelegate next, ILogger<ExceptionHandlingMiddleware> logger)
    {
        #region [- InvokeAsync -]
        public async Task InvokeAsync(HttpContext context)
        {
            try
            {
                await next(context);
            }

            catch (Exception ex)
            {
                logger.LogError(ex, "Unhandled exception");
                context.Response.StatusCode = StatusCodes.Status500InternalServerError;
                await context.Response.WriteAsJsonAsync(new ProblemDetails
                {
                    Status = StatusCodes.Status500InternalServerError,
                    Title = "Unexpected error"
                });
            }
        } 
        #endregion
    }
}
