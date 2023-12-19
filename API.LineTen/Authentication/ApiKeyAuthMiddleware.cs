namespace API.LineTen.Authentication
{
    public class ApiKeyAuthMiddleware
    {
        private readonly RequestDelegate _next;
        private const string ApiKeyHeaderName = "x-api-key";
        private const string ValidApiKey = "ec6254fd-ea6b-4f67-afc6-406c946ed854";

        public ApiKeyAuthMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task InvokeAsync(HttpContext context)
        {
            if (!context.Request.Headers.TryGetValue(ApiKeyHeaderName, out var extractedApiKey))
            {
                context.Response.StatusCode = 401;
                await context.Response.WriteAsync("API key missing");
                return;
            }

            if (!ValidApiKey.Equals(extractedApiKey))
            {
                context.Response.StatusCode = 401;
                await context.Response.WriteAsync("Invalid API key");
                return;
            }
            await _next(context);
        }
    }
}
