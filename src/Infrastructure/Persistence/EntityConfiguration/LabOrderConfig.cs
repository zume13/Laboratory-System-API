using Domain.Aggregates.Laboratory.LaboratoryOrder;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Persistence.Configurations;

public sealed class LaboratoryRequestOrderConfiguration
    : IEntityTypeConfiguration<LaboratoryRequestOrder>
{
    public void Configure(EntityTypeBuilder<LaboratoryRequestOrder> builder)
    {
        builder.ToTable("LaboratoryRequestOrders");

        builder.HasKey(x => x.Id);

        builder.Property(x => x.PatientId)
            .IsRequired();

        builder.Property(x => x.AppointmentId)
            .IsRequired();

        builder.Property(x => x.Status)
            .IsRequired()
            .HasConversion<string>();

        builder.Property(x => x.CreatedAt)
            .IsRequired();

        builder.Property(x => x.CompletedAt)
            .IsRequired(false);

        builder.HasMany<LaboratoryRequest>("_requests")
            .WithOne()
            .HasForeignKey(x => x.LabOrderId)
            .IsRequired()
            .OnDelete(DeleteBehavior.Cascade);

        builder.Navigation("_requests")
            .UsePropertyAccessMode(PropertyAccessMode.Field);
    }
}