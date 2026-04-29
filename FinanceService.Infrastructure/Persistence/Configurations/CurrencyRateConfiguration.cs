using FinanceService.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace FinanceService.Infrastructure.Persistence.Configurations;

/// <summary>
/// Конфигурация для EF таблица CurrencyRate
/// </summary>
public sealed class CurrencyRateConfiguration : IEntityTypeConfiguration<ExchangeRateEntity>
{
    public void Configure(EntityTypeBuilder<ExchangeRateEntity> builder)
    {
        builder.ToTable("CurrencyRate");
        
        builder.HasKey(x => x.Id);
        
        builder.Property(x => x.Name)
            .HasMaxLength(128)
            .IsRequired();

        builder.HasIndex(x => x.Name)
            .IsUnique();
        
        builder.Property(x => x.Rate)
            .IsRequired()
            .HasMaxLength(128);
    }
}