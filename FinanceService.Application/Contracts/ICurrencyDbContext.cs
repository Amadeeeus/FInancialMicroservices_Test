using BackgroundRateService.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace FinanceService.Application.Contracts;


public interface ICurrencyDbContext
{
    DbSet<ExchangeRateEntity> ExchangeRates { get; }
    Task<int> SaveChangesAsync(CancellationToken ct);
}