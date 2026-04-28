using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace UserServiceApplication;

/// <summary>
/// Регистрация зависимостей в слое Application
/// </summary>
public static class DependencyInjection
{
    public static IServiceCollection AddApplication(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddMediatR(cfg => 
            cfg.RegisterServicesFromAssembly(typeof(DependencyInjection)
                .Assembly));
        
        services
            .AddAutoMapper(cfg => 
                cfg.AddMaps(typeof(DependencyInjection).Assembly));
        
        return services;
    }
}