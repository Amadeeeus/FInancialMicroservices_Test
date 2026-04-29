using FinanceService.Application.Contracts;

using Refit;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace FinanceService.Infrastructure;

public static class DependencyInjection
{
    public static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddRefitClient<IUserServiceClient>()
            .ConfigureHttpClient(c =>
                c.BaseAddress = new Uri(configuration.GetSection("Services:UserService").Value!));
        
        return services;
    }
}