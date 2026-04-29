using BackgroundRateService.Application.Commands;
using BackgroundRateService.Application.Interfaces;
using FinanceService.Infrastructure.Persistence;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace BackgroundRateService.Application.Handlers;

/// <summary>
/// Хендлер обновления курсов
/// </summary>
/// <param name="context">контекст БД валют</param>
/// <param name="provider">Провайдер курса валют</param>
public class UpdateRatesHandler(CurrencyDbContext context, IExchangeRateProvider provider) : IRequestHandler<UpdateRatesCommand>
{
    public async Task Handle(UpdateRatesCommand command, CancellationToken ct)
    {
        var rates = await provider.GetExchangeRatesAsync(ct);

        var existingRates = await context.ExchangeRates.ToListAsync(ct);

        foreach (var (name, rate) in rates)
        {
            var  existing = existingRates.FirstOrDefault(x => x.Name == name);

            if (existing is null)
            {
                context.ExchangeRates.Add(new Domain.Entities.ExchangeRateEntity
                {
                    Name = name,
                    Rate = rate
                });
            }
            else
            {
                existing.Rate = rate;
            }
            
            await context.SaveChangesAsync(ct);
        }
    }
}