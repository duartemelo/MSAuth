namespace MSGym.API.Middlewares
{
    public class AppKeyValidationMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly List<string> _allowedAppKeys;

        public AppKeyValidationMiddleware(RequestDelegate next, IConfiguration configuration)
        {
            _next = next;
            _allowedAppKeys = configuration.GetSection("AllowedApps").Get<List<string>>()!;
        }

        public async Task InvokeAsync(HttpContext context)
        {
            if (!context.Request.Headers.TryGetValue("AppKey", out var extractedAppKey) || string.IsNullOrEmpty(extractedAppKey))
            {
                context.Response.StatusCode = StatusCodes.Status401Unauthorized;
                await context.Response.WriteAsync("AppKey header is missing.");
                return;
            }

            if (!_allowedAppKeys.Contains(extractedAppKey!))
            {
                context.Response.StatusCode = StatusCodes.Status401Unauthorized;
                await context.Response.WriteAsync("Unauthorized client.");
                return;
            }

            // If the AppKey is valid, proceed to the next middleware
            await _next(context);
        }
    }
}
