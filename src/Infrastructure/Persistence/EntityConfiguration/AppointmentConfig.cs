using Domain.Aggregates.Appointment;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Persistence.EntityConfiguration
{
    public class AppointmentConfig : IEntityTypeConfiguration<Appointment>
    {
        public void Configure(EntityTypeBuilder<Appointment> builder)
        {
            builder.ToTable("Appointments");
            builder.HasKey(x => x.Id);

            builder.Property(x => x.PatientId).IsRequired();
            builder.Property(x => x.AppointmentSlotId).IsRequired();
            builder.Property(x => x.Status).HasConversion<string>().HasMaxLength(20);
            builder.Property(x => x.BookingChannel).HasConversion<string>().HasMaxLength(20);
            builder.Property(x => x.CreatedAt).IsRequired();
            builder.Property(x => x.ConfirmedAt);

            builder.HasMany(x => x.Reminders)
                .WithOne()
                .HasForeignKey(r => r.AppointmentId)
                .OnDelete(DeleteBehavior.Cascade);

            builder.HasMany(x => x.Tests)
                .WithOne()
                .HasForeignKey(at => at.AppointmentId)
                .OnDelete(DeleteBehavior.Cascade);

            builder.Navigation(x => x.Tests)
                .HasField("_tests")
                .UsePropertyAccessMode(PropertyAccessMode.Field);

            builder.Navigation(x => x.Reminders)
                .HasField("_reminders")
                .UsePropertyAccessMode(PropertyAccessMode.Field);
        }
    }
}
