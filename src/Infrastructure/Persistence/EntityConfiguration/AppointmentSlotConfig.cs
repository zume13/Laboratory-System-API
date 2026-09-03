using Domain.Aggregates.AppointmentSlot;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Persistence.EntityConfiguration
{
    public class AppointmentSlotConfig : IEntityTypeConfiguration<AppointmentSlot>
    {
        public void Configure(EntityTypeBuilder<AppointmentSlot> builder)
        {
            builder.ToTable("AppointmentSlots");
            builder.HasKey(x => x.Id);

            builder.Property(x => x.Date).HasColumnType("timestamp without time zone").IsRequired();

            builder.OwnsOne(x => x.TimeRange, t =>
            {
                t.Property(p => p.Start).HasColumnName("StartTime").IsRequired();
                t.Property(p => p.End).HasColumnName("EndTime").IsRequired();
            });

            builder.Property(x => x.TestCategoryId);
            builder.Property(x => x.Capacity).IsRequired();
            builder.Property(x => x.BookedCount).IsRequired();
            builder.Property(x => x.ConfiguredByStaffId);

            builder.Ignore(x => x.IsFull);

            builder.HasIndex(x => new { x.Date, x.TestCategoryId });
        }
    }
}
