using Domain.Aggregates.Identity.AdministratorProfile;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Persistence.EntityConfiguration
{
    public class AdministratorProfileConfig : IEntityTypeConfiguration<AdministratorProfile>
    {
        public void Configure(EntityTypeBuilder<AdministratorProfile> builder)
        {
            builder.ToTable("AdministratorProfiles");
            builder.HasKey(x => x.Id);

            builder.Property(x => x.UserId).IsRequired();
            builder.HasIndex(x => x.UserId).IsUnique();

            builder.Property(x => x.Permissions).HasMaxLength(500);
        }
    }
}
