using System.Text;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using UserService.Infrastructure.Jwt.Implementations;
using UserService.Infrastructure.Jwt.Interfaces;
using UserService.Infrastructure.Jwt.Options;
using UserService.Infrastructure.Persistence;

namespace UserService.Infrastructure;

/// <summary>
/// Класс регистрации зависимостей
/// </summary>
public static class DependencyInjection
{
    public static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddDbContext<UserDbContext>(options =>
        {
            options.UseNpgsql(configuration
                    .GetConnectionString("UserDb"));
            
        });

        services.AddDbContext<TokensDbContext>(options =>
        {
            options.UseNpgsql(configuration.GetConnectionString("TokensDb"));
        });
        
        services.Configure<JwtTokenOptions>(configuration.GetRequiredSection("JwtTokenOptions"));
        
        services.AddScoped<IJwtTokenGenerator, JwtTokenGenerator>();
        
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
                        Encoding.UTF8.GetBytes(configuration["JwtTokenOptions:Secret"]!)),
                };
            });
        return services;
    }
}