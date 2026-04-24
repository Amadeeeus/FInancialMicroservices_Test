using Background.CurrencyRate.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;

namespace Background.CurrencyRate;

/// <summary>
/// Класс регистрации зависимостей
/// </summary>
public static class ServiceCollectionExtension
{
    public static void AddInfrastructure(this IServiceCollection collection, IConfiguration configuration)
        => collection.AddDbContext<FinancialDbContext>(options =>
        {
            options.UseNpgsql(configuration
                    .GetConnectionString("FinancialConnection"),
                npgsql =>
                {
                    npgsql
                        .MigrationsAssembly(typeof(FinancialDbContext)
                            .Assembly
                            .FullName);
                });
            options.EnableDetailedErrors();
        });
}
