using Background.CurrencyRate.BackgroundService;
using BackgroundRateService.Application;
using BackgroundRateService.Infrastructure;
using BackgroundRateService.Infrastructure.External;
using BackgroundRateService.Infrastructure.External.Interfaces;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Background.CurrencyRate;

/// <summary>
/// Передача зависимостей из Infrastructure
/// </summary>
public static class ServiceCollectionExtension
{
    public static void AddDependency(this IServiceCollection collection, IConfiguration configuration)
        => collection.AddInfrastructure(configuration)
            .AddApplication(configuration)
            .AddHttpClient<IExchangeRateProvider, ExchangeRateProvider>();

    public static void AddBackgroundService(this IServiceCollection collection, IConfiguration configuration)
        => collection.AddHostedService<RateUpdateBackgroundService>();
}
