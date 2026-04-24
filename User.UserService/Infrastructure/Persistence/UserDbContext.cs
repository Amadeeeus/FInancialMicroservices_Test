using Microsoft.EntityFrameworkCore;

namespace User.UserService.Infrastructure.Persistence;

public class UserDbContext(DbContextOptions<UserDbContext> options) : DbContext(options)
{
    public DbSet<Domain.Models.User>  Users { get; init; }
    
    public async 
}