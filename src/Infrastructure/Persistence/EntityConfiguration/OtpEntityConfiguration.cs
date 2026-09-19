using Domain.Aggregates.Otp;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Persistence.Configurations;

public class EmailVerificationOtpConfiguration
    : IEntityTypeConfiguration<Otp>
{
    public void Configure(
        EntityTypeBuilder<Otp> builder)
    {
        builder.ToTable("EmailVerificationOtps");

        builder.HasKey(x => x.Id);

        builder.Property(x => x.UserId)
            .IsRequired();

        builder.Property(x => x.OtpHash)
            .IsRequired()
            .HasMaxLength(255);

        builder.Property(x => x.ExpiresAt)
            .IsRequired();

        builder.Property(x => x.CreatedAt)
            .IsRequired();

        builder.Property(x => x.ConsumedAt);

        builder.Property(x => x.AttemptCount)
            .IsRequired();

        builder.HasIndex(x => x.UserId);
    }
}