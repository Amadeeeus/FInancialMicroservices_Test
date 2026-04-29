using FinanceService.Application.Contracts;
using Microsoft.EntityFrameworkCore;
using ExchangeRateEntity = BackgroundRateService.Domain.Entities.ExchangeRateEntity;

namespace FinanceService.Infrastructure.Persistence;

/// <summary>
/// Контекст БД Users
/// </summary>
/// <param name="options">Реализация базового конструктора</param>
public class CurrencyDbContext(DbContextOptions<CurrencyDbContext> options) : DbContext(options), ICurrencyDbContext
{
    public DbSet<ExchangeRateEntity> ExchangeRates { get; init; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder
            .ApplyConfigurationsFromAssembly(typeof(CurrencyDbContext).Assembly);
        
        base.OnModelCreating(modelBuilder);
    }
}