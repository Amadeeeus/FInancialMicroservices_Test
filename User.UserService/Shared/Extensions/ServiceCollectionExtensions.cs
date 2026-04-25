using System.Text;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using User.UserService.Infrastructure.Jwt.Implementations;
using User.UserService.Infrastructure.Jwt.Options;
using User.UserService.Infrastructure.Persistence;

namespace User.UserService.Shared.Extensions;

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