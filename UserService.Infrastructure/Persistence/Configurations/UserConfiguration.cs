using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using User.UserService.Domain.Models;

namespace UserService.Infrastructure.Persistence.Persistence.Configurations;

/// <summary>
/// Конфигурация для EF таблица Users
/// </summary>
public sealed class UserConfiguration : IEntityTypeConfiguration<UserEntity>
{
    public void Configure(EntityTypeBuilder<User.UserService.Domain.Models.UserEntity> builder)
    {
        builder.ToTable("Users");
        
        builder.HasKey(x => x.Id);
        
        builder.Property(x => x.Name)
            .HasMaxLength(128)
            .IsRequired();
        
        builder.Property(x => x.Password)
            .IsRequired().HasMaxLength(128);

        builder.Property(x => x.Favourites)
            .HasMaxLength(128);
    }
}