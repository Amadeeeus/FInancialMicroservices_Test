using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;

namespace UserService.Infrastructure.Persistence.Factory;

public class TokensDbContextFactory : IDesignTimeDbContextFactory<TokensDbContext>
{
    public TokensDbContext CreateDbContext(string[] args)
    {
        var config = new ConfigurationBuilder()
            .AddJsonFile(Path.Combine(Directory.GetCurrentDirectory(),
                "../UserService.Api/appsettings.json"))
            .Build();
        var options = new DbContextOptionsBuilder<TokensDbContext>()
            .UseNpgsql(config.GetConnectionString("TokensDb"))
            .Options;
        return new TokensDbContext(options);
    }
}