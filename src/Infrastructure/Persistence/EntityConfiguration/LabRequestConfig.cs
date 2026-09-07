using Domain.Aggregates.Laboratory.LaboratoryOrder;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Persistence.EntityConfiguration
{
    public sealed class LaboratoryRequestConfiguration
        : IEntityTypeConfiguration<LaboratoryRequest>
    {
        public void Configure(EntityTypeBuilder<LaboratoryRequest> builder)
        {
            builder.ToTable("LaboratoryRequests");

            builder.HasKey(x => x.Id);

            builder.Property(x => x.Id)
                .IsRequired();

            builder.Property(x => x.PatientId)
                .IsRequired();

            builder.Property(x => x.TestCategoryId)
                .IsRequired();

            builder.Property(x => x.Status)
                .IsRequired()
                .HasConversion<string>();

            builder.HasOne(x => x.labResult)
                .WithOne()
                .HasForeignKey<LaboratoryResult>(x => x.LaboratoryRequestId)
                .IsRequired()
                .OnDelete(DeleteBehavior.Cascade);

            builder.Navigation(x => x.labResult)
                .HasField("_result")
                .UsePropertyAccessMode(PropertyAccessMode.Field);
        }
    }
}