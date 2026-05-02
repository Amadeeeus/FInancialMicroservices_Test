namespace BackgroundRateService.Infrastructure.External.Interfaces;

/// <summary>
/// Контракт провайдера курсов
/// </summary>
public interface IExchangeRateProvider
{
    Task<Dictionary<string, decimal>> GetExchangeRatesAsync(CancellationToken cancellationToken);
}