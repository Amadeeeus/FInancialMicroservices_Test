using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;

namespace UserService.Infrastructure.Persistence.Factory;

public class UserDbContextFactory : IDesignTimeDbContextFactory<UserDbContext>
{
    public UserDbContext CreateDbContext(string[] args)
    {
        var config = new ConfigurationBuilder()
            .AddJsonFile(Path.Combine(Directory.GetCurrentDirectory(),
                "../UserService.Api/appsettings.json"))
            .Build();
        var options = new DbContextOptionsBuilder<UserDbContext>()
            .UseNpgsql(config.GetConnectionString("UserDb"))
            .Options;
        return new UserDbContext(options);
    }
}