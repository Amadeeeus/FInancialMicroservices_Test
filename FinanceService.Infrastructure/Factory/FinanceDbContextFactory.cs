using FinanceService.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;

namespace FinanceService.Infrastructure.Factory;

public class CurrencyDbContextFactory : IDesignTimeDbContextFactory<CurrencyDbContext>
{
    public CurrencyDbContext CreateDbContext(string[] args)
    {
        var config = new ConfigurationBuilder()
            .AddJsonFile(Path.Combine(Directory.GetCurrentDirectory(),
                "../FinanceService.Api/appsettings.json"))
            .Build();
        var options = new DbContextOptionsBuilder<CurrencyDbContext>()
            .UseNpgsql(config.GetConnectionString("CurrencyDb"))
            .Options;
        return new CurrencyDbContext(options);
    }
}