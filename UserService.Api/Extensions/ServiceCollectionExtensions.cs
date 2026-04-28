using System.Text;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using UserService.Infrastructure.Persistence;
using UserService.Infrastructure.Persistence.Jwt.Implementations;
using UserService.Infrastructure.Persistence.Jwt.Options;

namespace UserService.Api.Extensions;

/// <summary>
/// Класс регистрации зависимостей
/// </summary>
public static class ServiceCollectionExtensions
{
    public static void AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddA
    }
}