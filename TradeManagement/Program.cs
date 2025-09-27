using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using TradeManagement.Models;
using TradeManagement.Repositories;
using TradeManagement.Services;
using System.Text;

var builder = WebApplication.CreateBuilder(args);

// MongoDB settings from appsettings.json
var mongoSettings = builder.Configuration.GetSection("MongoDbSettings").Get<MongoDbSettings>()
                    ?? throw new Exception("MongoDB settings missing");

// Add services
builder.Services.AddSingleton(mongoSettings);
builder.Services.AddSingleton<IUserRepository, UserRepository>();

// JWT service
var jwtSettings = builder.Configuration.GetSection("Jwt");
builder.Services.AddSingleton(new JwtService(
    jwtSettings["Key"] ?? throw new Exception("JWT Key missing"),
    jwtSettings["Issuer"] ?? throw new Exception("JWT Issuer missing"),
    jwtSettings["Audience"] ?? throw new Exception("JWT Audience missing"),
    int.Parse(jwtSettings["ExpireMinutes"] ?? "60")
));

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// JWT Authentication
builder.Services.AddAuthentication(options =>
{
    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
})
.AddJwtBearer(options =>
{
    options.TokenValidationParameters = new TokenValidationParameters
    {
        ValidateIssuer = true,
        ValidateAudience = true,
        ValidateLifetime = true,
        ValidateIssuerSigningKey = true,
        ValidIssuer = jwtSettings["Issuer"],
        ValidAudience = jwtSettings["Audience"],
        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtSettings["Key"] ?? ""))
    };
});

// CORS
var corsOrigins = builder.Configuration.GetSection("Cors:AllowedOrigins").Get<string[]>() ?? new string[] { };
builder.Services.AddCors(options =>
{
    options.AddDefaultPolicy(policy =>
    {
        policy.WithOrigins(corsOrigins)
              .AllowAnyHeader()
              .AllowAnyMethod();
    });
});

var app = builder.Build();

app.UseSwagger();
app.UseSwaggerUI();

app.UseCors();
app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();
