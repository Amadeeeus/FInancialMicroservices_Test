using System.Text;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using UserService.Infrastructure.Persistence;
using UserService.Infrastructure.Persistence.Jwt.Implementations;
using UserService.Infrastructure.Persistence.Jwt.Options;

namespace Shared.Extensions;

/// <summary>
/// Класс регистрации зависимостей
/// </summary>
public static class ServiceCollectionExtensions
{
    public static void AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddDbContext<UserDbContext>(options =>
        {
            options.UseNpgsql(
                configuration
                    .GetConnectionString("DefaultConnection"),
                npgsql =>
                    npgsql
                        .MigrationsAssembly(typeof(UserDbContext)
                            .Assembly
                            .FullName));
        });
        
        services.Configure<JwtTokenOptions>(configuration.GetRequiredSection("JwtTokenOptions"));
        
        services.AddScoped<JwtTokenGenerator, JwtTokenGenerator>();
        
        services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
            .AddJwtBearer(options =>
            {
                options.TokenValidationParameters = new TokenValidationParameters()
                {
                    ValidateIssuer =  true,
                    ValidateAudience = true,
                    ValidateLifetime = true,
                    ValidateIssuerSigningKey = true,
                    
                    ValidIssuer = configuration["JwtTokenOptions:Issuer"],
                    ValidAudience = configuration["JwtTokenOptions:Audience"],
                    IssuerSigningKey = new SymmetricSecurityKey(
                        Encoding.UTF8.GetBytes(configuration["JwtTokenOptions:Key"]!)),
                };
            });
    }
}