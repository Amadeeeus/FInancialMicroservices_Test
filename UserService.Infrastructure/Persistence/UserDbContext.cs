using Microsoft.EntityFrameworkCore;

namespace UserService.Infrastructure.Persistence.Persistence;

/// <summary>
/// Контекст БД Users
/// </summary>
/// <param name="options">Реализация базового конструктора</param>
public class UserDbContext(DbContextOptions<UserDbContext> options) : DbContext(options)
{
    public DbSet<User.UserService.Domain.Models.UserEntity>  Users { get; init; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder
            .ApplyConfigurationsFromAssembly(typeof(UserDbContext).Assembly);
        
        base.OnModelCreating(modelBuilder);
    }
}