using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using User.UserService.Domain.Models;

namespace User.UserService.Infrastructure.Persistence.Configurations;

/// <summary>
/// Конфигурация контекста токена
/// </summary>
public class TokenConfiguration : IEntityTypeConfiguration<Token>
{
    public void Configure(EntityTypeBuilder<Token> builder)
    {
        builder.ToTable("Tokens");
        
        builder.HasKey(x => x.Id);
        
        builder.Property(x => x.RefreshToken)
            .IsRequired();
        
        builder.Property(x => x.Expires)
            .HasDefaultValue(
                DateTime
                    .UtcNow
                    .AddDays(7));
    }
}