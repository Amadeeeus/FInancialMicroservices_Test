using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace User.UserService.Infrastructure.Persistence.Configurations;

/// <summary>
/// Конфигурация для EF таблица Users
/// </summary>
public sealed class UserConfiguration : IEntityTypeConfiguration<Domain.Models.User>
{
    public void Configure(EntityTypeBuilder<Domain.Models.User> builder)
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