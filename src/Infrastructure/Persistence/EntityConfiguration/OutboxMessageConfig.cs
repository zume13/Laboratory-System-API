using Infrastructure.Persistence.Outbox;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Persistence.EntityConfiguration
{
    public sealed class OutboxMessageConfig : IEntityTypeConfiguration<OutBoxMessage>
    {
        public void Configure(EntityTypeBuilder<OutBoxMessage> builder)
        {
            builder.ToTable("OutboxMessages");

            builder.HasKey(x => x.Id);

            builder.Property(x => x.EventName)
                .IsRequired()
                .HasMaxLength(100);

            builder.Property(x => x.Type)
                .IsRequired()
                .HasMaxLength(200);

            builder.Property(x => x.Content)
                .IsRequired();

            builder.Property(x => x.OccurredOn)
                .HasColumnType("timestamp without time zone")
                .IsRequired();

            builder.Property(x => x.ProcessedOn)
                .HasColumnType("timestamp without time zone");

            builder.Property(x => x.Error)
                .HasMaxLength(1000);
        }
    }
}