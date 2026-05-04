using FinanceService.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace BackgroundRateService.Infrastructure;

/// <summary>
/// Подключение зависимостей в Program
/// </summary>
public static class DependencyInjection
{
    public static IServiceCollection AddInfrastructure(this IServiceCollection collection, IConfiguration configuration)
        => collection.AddDbContext<CurrencyDbContext>(options =>
        {
            options.UseNpgsql(configuration
                .GetConnectionString("CurrencyDb"));
            
            options.EnableDetailedErrors();
        });
}