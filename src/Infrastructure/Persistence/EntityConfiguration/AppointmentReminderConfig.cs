using Domain.Aggregates.Appointment;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Persistence.EntityConfiguration
{
    public class AppointmentReminderConfig : IEntityTypeConfiguration<AppointmentReminder>
    {
        public void Configure(EntityTypeBuilder<AppointmentReminder> builder)
        {
            builder.ToTable("AppointmentReminders");
            builder.HasKey(x => x.Id);

            builder.Property(x => x.AppointmentId).IsRequired();
            builder.Property(x => x.Channel).HasConversion<string>().HasMaxLength(20);
            builder.Property(x => x.ScheduledSendTime).IsRequired();
            builder.Property(x => x.Status).HasConversion<string>().HasMaxLength(20);
        }
    }
}
