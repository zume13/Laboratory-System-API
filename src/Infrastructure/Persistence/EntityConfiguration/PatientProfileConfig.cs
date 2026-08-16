using Domain.Aggregates.Identity.PatientProfile;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Persistence.EntityConfiguration
{
    public class PatientProfileConfig : IEntityTypeConfiguration<PatientProfile>
    {
        public void Configure(EntityTypeBuilder<PatientProfile> builder)
        {
            builder.ToTable("PatientProfiles");
            builder.HasKey(x => x.Id);

            builder.Property(x => x.UserId).IsRequired();
            builder.HasIndex(x => x.UserId).IsUnique();

            builder.Property(x => x.DateOfBirth).IsRequired();

            builder.Property(x => x.Sex).HasConversion<string>();

            builder.Property(x => x.PhysicalPatientId).HasMaxLength(50);

            builder.HasIndex(x => x.PhysicalPatientId);

            builder.Property(x => x.ConsentAccepted).IsRequired();
        }
    }
}
