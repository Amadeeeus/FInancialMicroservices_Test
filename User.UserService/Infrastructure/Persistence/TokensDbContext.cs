using Microsoft.EntityFrameworkCore;
using User.UserService.Domain.Models;

namespace User.UserService.Infrastructure.Persistence;

/// <summary>
/// Контекст Бд для хранения токена
/// </summary>
/// <param name="options"></param>
public class TokensDbContext(DbContextOptions<TokensDbContext> options) : DbContext(options)
{
    public DbSet<Token> Tokens {get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfigurationsFromAssembly(typeof(TokensDbContext).Assembly);
        
        base.OnModelCreating(modelBuilder);
    }
}