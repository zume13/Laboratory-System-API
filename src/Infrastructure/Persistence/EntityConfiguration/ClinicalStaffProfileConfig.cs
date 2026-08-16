using Domain.Aggregates.Identity.ClinicalStaffProfile;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Persistence.EntityConfiguration
{
    public class ClinicalStaffProfileConfig : IEntityTypeConfiguration<ClinicalStaffProfile>
    {
        public void Configure(EntityTypeBuilder<ClinicalStaffProfile> builder)
        {
            builder.ToTable("ClinicalStaffProfiles");
            builder.HasKey(x => x.Id);

            builder.Property(x => x.UserId).IsRequired();
            builder.HasIndex(x => x.UserId).IsUnique();

            builder.Property(x => x.Role).HasConversion<string>().HasMaxLength(50);
            builder.Property(x => x.IsActive).IsRequired();
        }
    }
}
