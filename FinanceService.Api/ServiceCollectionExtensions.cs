using System.Text;
using FinanceService.Infrastructure;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;

namespace FinanceService.Api;

/// <summary>
/// Передача зависимостей из Infrastructure
/// </summary>
public static class ServiceCollectionExtensions
{
    public static void AddDependencies(this IServiceCollection services, IConfiguration configuration) 
        => services.AddInfrastructure(configuration);

    public static void AddJwtAuthentication(this IServiceCollection services, IConfiguration configuration)
        => services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
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
}