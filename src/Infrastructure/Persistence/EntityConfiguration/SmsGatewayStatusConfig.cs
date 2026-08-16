using Domain.Aggregates.Communications.SmsGateway;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Persistence.EntityConfiguration
{
    public class SmsGatewayStatusConfig : IEntityTypeConfiguration<SmsGatewayStatus>
    {
        public void Configure(EntityTypeBuilder<SmsGatewayStatus> builder)
        {
            builder.ToTable("SmsGatewayStatuses");
            builder.HasKey(x => x.Id);

            builder.Property(x => x.Status).HasMaxLength(50);
            builder.Property(x => x.CreditsRemaining).IsRequired();
            builder.Property(x => x.ErrorCount24h).IsRequired();
            builder.Property(x => x.LastUpdated).IsRequired();
        }
    }
}
