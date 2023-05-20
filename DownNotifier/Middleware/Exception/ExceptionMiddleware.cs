using System.Net;

namespace DownNotifier.Middleware.Exception
{
    public class ExceptionMiddleware
    {
        private readonly RequestDelegate _next; 
        public ExceptionMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task InvokeAsync(HttpContext httpContext)
        {
            try
            {
                await _next(httpContext);
            }
            catch (System.Exception ex)
            {
                await HandleExceptionAsync(httpContext, ex);
            }
        }

        private static Task HandleExceptionAsync(HttpContext httpContext, System.Exception ex)
        {

            httpContext.Response.ContentType = "application/json";
            httpContext.Response.StatusCode = (int)HttpStatusCode.InternalServerError;
            var now = DateTime.UtcNow; 
            return httpContext.Response.WriteAsync(new 
            {
                Status = false,
                Message = ex.Message
            }.ToString());
        }
    }
}
