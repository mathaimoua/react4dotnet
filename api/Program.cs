using Microsoft.EntityFrameworkCore;
using api.Data;

using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Text;

// Program.cs setup
var builder = WebApplication.CreateBuilder(args); // This starts up the .NET application and gives you a builder object to register all your services before the app runs. Think of it as the setup phase.

builder.Services.AddControllers() // This tells .NET "I have controllers that handle HTTP requests, please set them up." Without this your routes won't work.
    .AddJsonOptions(options =>
    {
        options.JsonSerializerOptions.PropertyNamingPolicy = System.Text.Json.JsonNamingPolicy.SnakeCaseLower; // makes .NET automatically convert between FirstName in C# and first_name in JSON so React and API speak the same naming convention.
    });

builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseNpgsql(builder.Configuration.GetConnectionString("DefaultConnection")));

// JWT Setup
var jwtSettings = builder.Configuration.GetSection("JwtSettings"); // Reads the JWT settings from appsettings.json
var secretKey = jwtSettings["SecretKey"]; // Registers JWT as the authentication method

builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddJwtBearer(options =>
    {
        options.TokenValidationParameters = new TokenValidationParameters // Rules the API uses to decide if a token is valid or not. Each Validate line adds a check
        {
            ValidateIssuer = true,
            ValidateAudience = true,
            ValidateLifetime = true,
            ValidateIssuerSigningKey = true,
            ValidIssuer = jwtSettings["Issuer"],
            ValidAudience = jwtSettings["Audience"],
            IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(secretKey)) // Converts it into a cryptographic key
        };
    });

// Start building Program.cs
builder.Services.AddAuthorization();

builder.Services.AddCors(options =>
{
    options.AddDefaultPolicy(policy =>
        policy.WithOrigins("http://localhost:3000") // Allow connections from here
              .AllowAnyHeader()
              .AllowAnyMethod());
});

var app = builder.Build(); // Creates the actual running application

// ORDER MATTERS
app.UseCors();
app.UseAuthentication();
app.UseAuthorization();
app.MapControllers(); // Wire up all the routes automatically.
app.Run();