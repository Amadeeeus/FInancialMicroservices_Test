using BackgroundRateService.Application.Commands;
using MediatR;

namespace Background.CurrencyRate.BackgroundService;

/// <summary>
/// Background service для получения курса валют
/// </summary>
/// <param name="provider"></param>
public class RateUpdateBackgroundService(IServiceProvider provider) : Microsoft.Extensions.Hosting.BackgroundService
{
    protected override async Task ExecuteAsync(CancellationToken ct)
    {
        while (!ct.IsCancellationRequested)
        {
            using var scope = provider.CreateScope();
            var mediator = scope.ServiceProvider.GetRequiredService<IMediator>();

            await mediator.Send(new UpdateRatesCommand(), ct);
            
            await Task.Delay(TimeSpan.FromMinutes(30), ct);
        }
    }
}