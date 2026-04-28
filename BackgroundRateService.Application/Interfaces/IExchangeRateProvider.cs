namespace BackgroundRateService.Application.Interfaces;

/// <summary>
/// Контракт провайдера курсов
/// </summary>
public interface IExchangeRateProvider
{
    Task<Dictionary<string, decimal>> GetExchangeRatesAsync(CancellationToken cancellationToken);
}