using Domain.Aggregates.Laboratory.LaboratoryOrder;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

public sealed class LabRequestConfig
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

        builder.HasOne<LaboratoryResult>("_result")
            .WithOne()
            .HasForeignKey<LaboratoryResult>(x => x.LaboratoryRequestId)
            .IsRequired()
            .OnDelete(DeleteBehavior.Cascade);

        builder.Navigation("_result")
            .UsePropertyAccessMode(PropertyAccessMode.Field);
    }
}