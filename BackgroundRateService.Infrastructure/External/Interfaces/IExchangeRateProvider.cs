using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace BackgroundRateService.Infrastructure.External.Interfaces;

/// <summary>
/// Контракт провайдера курсов
/// </summary>
public interface IExchangeRateProvider
{
    Task<Dictionary<string, decimal>> GetExchangeRatesAsync(CancellationToken cancellationToken);
}