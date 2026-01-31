using CrudApi.API.Middlewares;
using CrudApi.Application.Interfaces.Repositories;
using CrudApi.Application.Interfaces.Services;
using CrudApi.Application.Mapping;
using CrudApi.Application.Services;
using CrudApi.Application.Settings;
using CrudApi.Infrastructure.Data;
using CrudApi.Infrastructure.Identity;
using CrudApi.Infrastructure.Repositories;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.OpenApi;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Scalar.AspNetCore;
using System.Text;

var builder = WebApplication.CreateBuilder(args);

#region Controllers
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddOpenApi();
#endregion

#region AutoMapper
builder.Services.AddAutoMapper(typeof(ProdutoProfile));
#endregion

#region Database
var connectionString =
    Environment.GetEnvironmentVariable("CONNECTION_STRING")
    ?? builder.Configuration.GetConnectionString("DefaultConnection");

if (string.IsNullOrWhiteSpace(connectionString))
    throw new InvalidOperationException("Connection string não configurada.");

builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseSqlServer(connectionString));
#endregion

#region Identity
builder.Services
    .AddIdentity<ApplicationUser, IdentityRole>(options =>
    {
        options.Password.RequireDigit = true;
        options.Password.RequireLowercase = true;
        options.Password.RequireUppercase = true;
        options.Password.RequireNonAlphanumeric = false;
        options.Password.RequiredLength = 6;
    })
    .AddEntityFrameworkStores<AppDbContext>()
    .AddDefaultTokenProviders();
#endregion

#region Repositories & Services
builder.Services.AddScoped(typeof(IRepository<>), typeof(Repository<>));
builder.Services.AddScoped<IProdutoRepository, ProdutoRepository>();
builder.Services.AddScoped<IAuthService, AuthService>();
builder.Services.AddScoped<ITokenService, TokenService>();
#endregion

#region JWT Settings
builder.Services.Configure<JWTSettings>(
    builder.Configuration.GetSection("JWTSettings"));

var jwtSettings = builder.Configuration
    .GetSection("JWTSettings")
    .Get<JWTSettings>()
    ?? throw new InvalidOperationException("JWTSettings não configurado.");
#endregion

#region Authentication & Authorization
builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddJwtBearer(options =>
    {
        options.TokenValidationParameters = new TokenValidationParameters
        {
            ValidateIssuer = true,
            ValidateAudience = true,
            ValidateLifetime = true,
            ValidateIssuerSigningKey = true,
            ValidIssuer = jwtSettings.Issuer,
            ValidAudience = jwtSettings.Audience,
            IssuerSigningKey = new SymmetricSecurityKey(
                Encoding.UTF8.GetBytes(jwtSettings.Secret))
        };
    });

builder.Services.AddAuthorization();
#endregion

var app = builder.Build();

#region OpenAPI + Scalar
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();

    app.MapScalarApiReference(options =>
    {
        options
            .WithTitle("CrudApi")
            .WithTheme(ScalarTheme.Moon)
            .WithDefaultHttpClient(
                ScalarTarget.CSharp,
                ScalarClient.HttpClient);
    });
}
#endregion

#region Middlewares
app.UseMiddleware<GlobalExceptionMiddleware>();

app.UseAuthentication();
app.UseAuthorization();
#endregion

#region Endpoints
app.MapControllers();
#endregion

app.Run();
