using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using UserService.Domain.Entities;

namespace UserService.Infrastructure.Persistence.Configurations;

/// <summary>
/// Конфигурация для EF таблица Users
/// </summary>
public class UserConfiguration : IEntityTypeConfiguration<UserEntity>
{
    public void Configure(EntityTypeBuilder<UserEntity> builder)
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