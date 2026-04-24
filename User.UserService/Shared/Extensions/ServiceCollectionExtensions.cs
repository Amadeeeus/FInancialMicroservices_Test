using Microsoft.EntityFrameworkCore;
using User.UserService.Infrastructure.Persistence;

namespace User.UserService.Shared.Extensions;

/// <summary>
/// Класс регистрации зависимостей
/// </summary>
public static class ServiceCollectionExtensions
{
    public static void AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
        => services.AddDbContext<UserDbContext>(options =>
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
}