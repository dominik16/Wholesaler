using Wholesaler.Exceptions;

namespace Wholesaler.Middelware
{
    public class ErrorHandlingMiddleware : IMiddleware
    {
        private readonly ILogger<ErrorHandlingMiddleware> _logger;

        public ErrorHandlingMiddleware(ILogger<ErrorHandlingMiddleware> logger)
        {
            _logger = logger;
        }

        public async Task InvokeAsync(HttpContext context, RequestDelegate next)
        {
            try
            {
                await next.Invoke(context);
            }
            catch(BadRequestException badRequest)
            {
                context.Response.StatusCode = 400;
                await context.Response.WriteAsync(badRequest.Message);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.StackTrace);
                context.Response.StatusCode= 500;
                await context.Response.WriteAsync("Something went wrong");
            }
        }
    }
}
