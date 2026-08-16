using Domain.Aggregates.Communications.Notification;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Persistence.EntityConfiguration
{
    public class NotificationConfig : IEntityTypeConfiguration<Notification>
    {
        public void Configure(EntityTypeBuilder<Notification> builder)
        {
            builder.ToTable("Notifications");
            builder.HasKey(x => x.Id);

            builder.Property(x => x.PatientId).IsRequired();
            builder.Property(x => x.LabResultId);
            builder.Property(x => x.Channel).HasConversion<string>().HasMaxLength(20);
            builder.Property(x => x.Status).HasConversion<string>().HasMaxLength(20);
            builder.Property(x => x.Message).IsRequired().HasMaxLength(500);
            builder.Property(x => x.SentAt);
            builder.Property(x => x.CreatedAt).IsRequired();
        }
    }
}
