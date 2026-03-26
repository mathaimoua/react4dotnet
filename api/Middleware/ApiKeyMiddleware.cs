namespace api.Middleware;

public class ApiKeyMiddleware
{
    private readonly RequestDelegate _next;
    private const string ApiKeyHeader = "x-api-key"; // The header name React will send the API key in

    public ApiKeyMiddleware(RequestDelegate next)
    {
        _next = next;
    }

    public async Task InvokeAsync(HttpContext context, IConfiguration config)
    {
        // Check if the header exists at all
        if (!context.Request.Headers.TryGetValue(ApiKeyHeader, out var key))
        {
            context.Response.StatusCode = 401;
            await context.Response.WriteAsync("API key missing");
            return;
        }

        // Check if the key matches the one in appsettings
        var validKey = config["ApiKey"];
        if (key != validKey)
        {
            context.Response.StatusCode = 401;
            await context.Response.WriteAsync("Invalid API key");
            return;
        }

        // Key is valid, pass the request through to the controller
        await _next(context);
    }
}