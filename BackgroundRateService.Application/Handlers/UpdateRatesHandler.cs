using BackgroundRateService.Application.Commands;
using BackgroundRateService.Infrastructure.External.Interfaces;
using FinanceService.Domain.Entities;
using FinanceService.Infrastructure.Persistence;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace BackgroundRateService.Application.Handlers;

/// <summary>
/// Хендлер обновления курсов
/// </summary>
/// <param name="context">контекст БД валют</param>
/// <param name="provider">Провайдер курса валют</param>
public class UpdateRatesHandler(CurrencyDbContext context, IExchangeRateProvider provider, ILogger<UpdateRatesHandler> logger) : IRequestHandler<UpdateRatesCommand>
{
    public async Task Handle(UpdateRatesCommand command, CancellationToken ct)
    {
        
        logger.LogInformation("Exchange rates update started | {Time}", DateTime.UtcNow);
        
        var rates = await provider.GetExchangeRatesAsync(ct);

        logger.LogInformation("Rates fetched from CBR | Count: {Count}", rates.Count);
        
        var existingRates = await context.ExchangeRates.ToListAsync(ct);
        
        foreach (var (name, rate) in rates)
        {
            var  existing = existingRates.FirstOrDefault(x => x.Name == name);

            if (existing is null)
            {
                context.ExchangeRates.Add(new ExchangeRateEntity
                {
                    Name = name,
                    Rate = rate
                });
                
                logger.LogInformation("Rate processed | Name: {Name} Rate: {Rate}", name, rate);
            }
            else
            {
                existing.Rate = rate;
                
                logger.LogInformation("Exchange rates update completed | {Time}", DateTime.UtcNow);
            }
            
            await context.SaveChangesAsync(ct);
        }
    }
}