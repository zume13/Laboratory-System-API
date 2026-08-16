using Domain.Aggregates.Monitoring.StorageStatus;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Persistence.EntityConfiguration
{
    public class StorageStatusConfig : IEntityTypeConfiguration<StorageStatus>
    {
        public void Configure(EntityTypeBuilder<StorageStatus> builder)
        {
            builder.ToTable("StorageStatuses");
            builder.HasKey(x => x.Id);

            builder.Property(x => x.StorageType).IsRequired().HasMaxLength(50);
            builder.Property(x => x.UsedGb).HasColumnType("decimal(10,2)");
            builder.Property(x => x.CapacityGb).HasColumnType("decimal(10,2)");
            builder.Property(x => x.LastCheckedAt).IsRequired();

            builder.Ignore(x => x.PercentUsed);
        }
    }
}
