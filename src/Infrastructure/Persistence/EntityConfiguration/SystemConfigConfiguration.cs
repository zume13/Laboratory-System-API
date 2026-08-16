using Domain.Aggregates.Monitoring.SystemConfig;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Persistence.EntityConfiguration
{
    public class SystemConfigConfiguration : IEntityTypeConfiguration<SystemConfig>
    {
        public void Configure(EntityTypeBuilder<SystemConfig> builder)
        {
            builder.ToTable("SystemConfigs");
            builder.HasKey(x => x.Id);

            builder.Property(x => x.Key).IsRequired().HasMaxLength(100);
            builder.HasIndex(x => x.Key).IsUnique();
            builder.Property(x => x.Value);
            builder.Property(x => x.UpdatedAt).IsRequired();
        }
    }
}
