using Domain.Aggregates.LaboratoryOrder;
using Domain.Aggregates.LaboratoryOrder.LaboratoryRequest;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Persistence.EntityConfiguration
{
    public class LabRequestConfig : IEntityTypeConfiguration<LaboratoryRequest>
    {
        public void Configure(EntityTypeBuilder<LaboratoryRequest> builder)
        {
            builder.ToTable("LabRequests");
            builder.HasKey(x => x.Id);

            builder.Property(x => x.PhysicalPatientId).HasMaxLength(50);
            builder.HasIndex(x => x.PhysicalPatientId);

            builder.Property(x => x.TestCategoryId).IsRequired();
            builder.Property(x => x.Status).HasConversion<string>().HasMaxLength(20);
            builder.Property(x => x.CreatedAt).IsRequired();

            // LabResult is a child entity within this aggregate's boundary — mapped
            // via the private backing field, not exposed as its own DbSet.
            builder.HasMany(x => x.Results)
                .WithOne()
                .HasForeignKey(r => r.LaboratoryRequestId)
                .OnDelete(DeleteBehavior.Cascade);

            builder.Navigation(x => x.Results)
                .HasField("_results")
                .UsePropertyAccessMode(PropertyAccessMode.Field);
        }
    }
}
