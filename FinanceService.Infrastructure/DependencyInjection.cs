using FinanceService.Application.Contracts;
using FinanceService.Infrastructure.Handlers;
using FinanceService.Infrastructure.Persistence;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Refit;

namespace FinanceService.Infrastructure;

public static class DependencyInjection
{
    public static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddTransient<AuthHeaderHandler>();
        
        services.AddRefitClient<IUserServiceClient>()
            .ConfigureHttpClient(c =>
                c.BaseAddress = new Uri(configuration.GetSection("Services:UserService").Value!))
            .AddHttpMessageHandler<AuthHeaderHandler>();
        
        services.AddNpgsql<CurrencyDbContext>(configuration
            .GetConnectionString("CurrencyDb"));
        
        services.AddScoped<ICurrencyDbContext, CurrencyDbContext>();
        return services;
    }
}