using Domain.Aggregates.Monitoring.ActivityLog;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Persistence.EntityConfiguration
{
    public class ActivityLogConfig : IEntityTypeConfiguration<ActivityLog>
    {
        public void Configure(EntityTypeBuilder<ActivityLog> builder)
        {
            builder.ToTable("ActivityLogs");
            builder.HasKey(x => x.Id);

            builder.Property(x => x.UserId);
            builder.Property(x => x.Action).IsRequired().HasMaxLength(200);
            builder.Property(x => x.Target).HasMaxLength(200);
            builder.Property(x => x.Severity).HasConversion<string>().HasMaxLength(20);
            builder.Property(x => x.Timestamp).IsRequired();

            builder.HasIndex(x => x.Timestamp);
        }
    }
}
