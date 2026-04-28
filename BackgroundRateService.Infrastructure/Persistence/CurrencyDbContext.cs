using BackgroundRateService.Domain.Entities;
using Microsoft.EntityFrameworkCore;


namespace BackgroundRateService.Infrastructure.Persistence;

/// <summary>
/// Контекст БД Users
/// </summary>
/// <param name="options">Реализация базового конструктора</param>
public class CurrencyDbContext(DbContextOptions<CurrencyDbContext> options) : DbContext(options)
{
    public DbSet<ExchangeRateEntity> ExchangeRates { get; init; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder
            .ApplyConfigurationsFromAssembly(typeof(CurrencyDbContext).Assembly);
        
        base.OnModelCreating(modelBuilder);
    }
}