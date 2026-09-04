using Domain.Aggregates.LaboratoryOrder.LaboratoryRequest;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Persistence.EntityConfiguration
{
    public class LabResultConfig : IEntityTypeConfiguration<LaboratoryResult>
    {
        public void Configure(EntityTypeBuilder<LaboratoryResult> builder)
        {
            builder.ToTable("LabResults");
            builder.HasKey(x => x.Id);

            builder.Property(x => x.LaboratoryRequestId).IsRequired();
            builder.Property(x => x.UploadedByStaffId).IsRequired();

            builder.OwnsOne(x => x.PdfPath, p =>
                p.Property(v => v.value).HasColumnName("PdfFilePath").IsRequired().HasMaxLength(300));

            builder.Property(x => x.SampleId).HasMaxLength(50);
            builder.Property(x => x.ReleaseDate);
            builder.Property(x => x.IsVoided).IsRequired();
            builder.Property(x => x.UploadedAt).IsRequired();
        }
    }
}
