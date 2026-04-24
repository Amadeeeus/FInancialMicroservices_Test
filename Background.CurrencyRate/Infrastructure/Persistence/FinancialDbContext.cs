using Background.CurrencyRate.Models;
using Microsoft.EntityFrameworkCore;

namespace Background.CurrencyRate.Infrastructure.Persistence;

/// <summary>
/// Контекст БД Users
/// </summary>
/// <param name="options">Реализация базового конструктора</param>
public class FinancialDbContext(DbContextOptions<FinancialDbContext> options) : DbContext(options)
{
    public DbSet<Financial> Financials { get; init; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder
            .ApplyConfigurationsFromAssembly(typeof(FinancialDbContext).Assembly);
        
        base.OnModelCreating(modelBuilder);
    }
}