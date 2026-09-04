using Domain.Aggregates.Laboratory.LaboratoryOrder;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Persistence.Configurations;

public sealed class LaboratoryResultConfiguration
    : IEntityTypeConfiguration<LaboratoryResult>
{
    public void Configure(EntityTypeBuilder<LaboratoryResult> builder)
    {
        builder.ToTable("LaboratoryResults");

        builder.HasKey(x => x.Id);

        builder.Property(x => x.LaboratoryRequestId)
            .IsRequired();

        builder.Property(x => x.UploadedByStaffId)
            .IsRequired();

        builder.Property(x => x.SampleId)
            .IsRequired()
            .HasMaxLength(100);

        builder.Property(x => x.UploadedAt)
            .IsRequired();

        builder.Property(x => x.ReleaseDate)
            .IsRequired(false);

        builder.Property(x => x.IsVoided)
            .IsRequired();

        builder.OwnsOne(x => x.PdfPath, pdfPath =>
        {
            pdfPath.Property(x => x.value)
                .HasColumnName("PdfPath")
                .IsRequired()
                .HasMaxLength(500);
        });

        builder.HasOne<LaboratoryRequest>()
            .WithOne()
            .HasForeignKey<LaboratoryResult>(x => x.LaboratoryRequestId)
            .IsRequired()
            .OnDelete(DeleteBehavior.Cascade);

        // Prevent multiple results for the same request
        builder.HasIndex(x => x.LaboratoryRequestId)
            .IsUnique();
    }
}