namespace JwtAuthApi.Middlewares
{
    public class ExceptionMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly ILogger<ExceptionMiddleware> _logger;

        public ExceptionMiddleware(ILogger<ExceptionMiddleware> logger, RequestDelegate next)
        {
            _logger = logger;
            _next = next;
        }

        public async Task InvokeAsync(HttpContext context)
        {
            try
            {
                await _next(context);

                if (context.Response.StatusCode == 401)
                {
                    _logger.LogWarning("Unauthorized access attempt");
                    context.Response.ContentType = "application/json";
                    await context.Response.WriteAsync("{\"message\":\"未登入或Token無效\"}");
                }

                if (context.Response.StatusCode == 403)
                {
                    _logger.LogWarning("Forbidden access attempt");
                    context.Response.ContentType = "application/json";
                    await context.Response.WriteAsync("{\"message\":\"權限不足\"}");
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Unhandled exception occurred");
                context.Response.StatusCode = 500;
                context.Response.ContentType = "application/json";
                await context.Response.WriteAsync("{\"message\":\"系統錯誤\"}");
            }
        }
    }
}
