using Domain.Aggregates.Identity.AdministratorProfile;
using Domain.Aggregates.Identity.UserProfile;
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

            builder.HasOne<User>()
                   .WithOne()
                   .HasForeignKey<AdministratorProfile>(x => x.UserId);

        }
    }
}
