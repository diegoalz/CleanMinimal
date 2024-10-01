using CleanMinimal.Domain.Common;
using CleanMinimal.Domain.Models;
using CleanMinimal.Domain.ValueObjects;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CleanMinimal.Infraestructure.Persistence.Configuration;

public class UserConfiguration : IEntityTypeConfiguration<User>
{
    public void Configure(EntityTypeBuilder<User> builder)
    {
        builder.ToTable("Users");
        builder.HasKey(u => u.Id);
        builder.Ignore(u => u.FullName);
        // builder.Ignore(u => u.sales);
        builder.HasIndex(u => u.Email).IsUnique();
        
        builder.Property(u => u.Id).HasConversion(
            BaseId => BaseId.Value,
            value => new BaseId(value)
        );
        builder.Property(u => u.Name)
            .HasMaxLength(50);
        builder.Property(u => u.LastName).HasMaxLength(80);
        builder.Property(u => u.Email).HasConversion(
            email => email.Value,
            value => Email.Create(value)!
        ).HasMaxLength(255);
        builder.Property(u => u.PhoneNumber).HasConversion(
            phoneNumber => phoneNumber.Value,
            value => PhoneNumber.Create(value)!
        ).HasMaxLength(13);
        builder.Property(u => u.Active);

    }
}