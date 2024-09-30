using CleanMinimal.Domain.Common;
using CleanMinimal.Domain.Models;
using CleanMinimal.Domain.ValueObjects;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CleanMinimal.Infraestructure.Persistence.Configuration;

public class SaleConfiguration : IEntityTypeConfiguration<Sale>
{
    public void Configure(EntityTypeBuilder<Sale> builder)
    {
        builder.ToTable("Sales");
        builder.HasKey(s => s.Id);
        builder.Ignore(s => s.FormattedAmount);
        builder.Property(s => s.Id).HasConversion(
            BaseId => BaseId.Value,
            value => new BaseId(value)
        );
        builder.Property(s => s.Amount);
        builder.Property(s => s.Concept)
            .HasMaxLength(100);
        builder.Property(s => s.SaleDateTime)
            .HasConversion(
                dateTime => dateTime.Value,
                value => new CustomDateTime(value)
            );
        builder.Property(s => s.UserId);
    }
}