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
            var environment = Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT");

            // Skip validation in the Development environment
            if (environment == "Development")
            {
                await _next(context);
                return;
            }

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
