using Background.CurrencyRate.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Background.CurrencyRate.Infrastructure.Persistence.Configurations;

/// <summary>
/// Конфигурация для EF таблица Users
/// </summary>
public sealed class UserConfiguration : IEntityTypeConfiguration<Financial>
{
    public void Configure(EntityTypeBuilder<Financial> builder)
    {
        builder.ToTable("Financials");
        
        builder.HasKey(x => x.Id);
        
        builder.Property(x => x.Name)
            .HasMaxLength(128)
            .IsRequired();
        
        builder.Property(x => x.Rate)
            .IsRequired().HasMaxLength(128);
    }
}