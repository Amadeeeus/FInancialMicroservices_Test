using BackgroundRateService.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;
using UserService.Infrastructure.Persistence;

namespace Db.Migrations;

/// <summary>
/// Hosted service для миграций
/// </summary>
/// <param name="serviceProvider">Провайдер области видимости сервиса</param>
/// <param name="logger">Логирование</param>
/// <param name="lifetime">Управление жизненным циклом сервиса</param>
public class MigrationHostedService(IServiceProvider serviceProvider, ILogger<MigrationHostedService> logger, IHostApplicationLifetime lifetime) : IHostedService
{
    public async Task StartAsync(CancellationToken ct)
    {
        try
        {
            logger.LogInformation("Старт сервиса миграций");

            using var scope = serviceProvider.CreateScope();

            var userDb = scope.ServiceProvider.GetRequiredService<UserDbContext>();
            var tokenDb =  scope.ServiceProvider.GetRequiredService<TokensDbContext>();
            var financialDb = scope.ServiceProvider.GetRequiredService<CurrencyDbContext>();

            logger.LogInformation("Применение миграции UserDb");
            await userDb.Database.MigrateAsync(ct);

            logger.LogInformation("Применение миграции TokenDb");
            await tokenDb.Database.MigrateAsync(ct);
            
            logger.LogInformation("Применение миграции FinancialDb");
            await financialDb.Database.MigrateAsync(ct);

            logger.LogInformation("Миграция успешно завершена");
        }
        catch (Exception e)
        {
            logger.LogInformation("Миграция не завершена");
            throw;
        }
        finally
        {
            lifetime.StopApplication();
        }
    }
    
    public Task StopAsync(CancellationToken cancellationToken) => Task.CompletedTask;
}