using Domain.Aggregates.SlotCapacity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Persistence.EntityConfiguration
{
    public class SlotCapacityConfigConfig : IEntityTypeConfiguration<SlotCapacityConfig>
    {
        public void Configure(EntityTypeBuilder<SlotCapacityConfig> builder)
        {
            builder.ToTable("SlotCapacityConfigs");
            builder.HasKey(x => x.Id);

            builder.Property(x => x.TestCategoryId).IsRequired();
            builder.HasIndex(x => x.TestCategoryId).IsUnique();
            builder.Property(x => x.MaxDailyBookings).IsRequired();
            builder.Property(x => x.MaxPerSlot).IsRequired();
        }
    }
}
