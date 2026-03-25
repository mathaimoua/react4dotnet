using Microsoft.EntityFrameworkCore;
using api.Data;

var builder = WebApplication.CreateBuilder(args); // Starts up the .NET application and gives you a builder object to register all your services before the app runs. Think of it as the setup phase.

builder.Services.AddControllers() // Tells .NET "I have controllers that handle HTTP requests, please set them up." Without this your routes won't work.
    .AddJsonOptions(options =>
    {
        options.JsonSerializerOptions.PropertyNamingPolicy = System.Text.Json.JsonNamingPolicy.SnakeCaseLower; // Makes .NET automatically convert between FirstName in C# and first_name in JSON so React and API speak the same naming convention.
    });

builder.Services.AddDbContext<AppDbContext>(options => // Registers Entity Framework with your connection string so it's available throughout the app.
    options.UseNpgsql(builder.Configuration.GetConnectionString("DefaultConnection")));

builder.Services.AddCors(options =>
{
    options.AddDefaultPolicy(policy =>
        policy.WithOrigins("http://localhost:3000") // Only allows requests from this origin. Update this per environment (DEV/QA/Prod).
              .AllowAnyHeader()
              .AllowAnyMethod());
});

var app = builder.Build(); // Takes all the configuration above and creates the actual running application.

// ORDER MATTERS - middleware runs in the order it's registered
app.UseCors(); // Must run first so the browser doesn't block requests before they reach auth
app.UseMiddleware<api.Middleware.ApiKeyMiddleware>(); // Checks every request for a valid API key before it reaches any controller
app.MapControllers(); // Wires up all routes in the Controllers folder automatically
app.Run();