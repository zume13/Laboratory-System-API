using Domain.Aggregates.Identity.User;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Persistence.EntityConfiguration
{
    public class UserConfig : IEntityTypeConfiguration<User>
    {
        public void Configure(EntityTypeBuilder<User> builder)
        {
            builder.ToTable("Users");
            builder.HasKey(x => x.Id);

            builder.OwnsOne(x => x.FirstName, n =>
                n.Property(p => p.value).HasColumnName("FirstName").IsRequired().HasMaxLength(100));

            builder.OwnsOne(x => x.LastName, n =>
                n.Property(p => p.value).HasColumnName("LastName").IsRequired().HasMaxLength(100));

            builder.OwnsOne(x => x.Email, e =>
            {
                e.Property(p => p.value).HasColumnName("Email").IsRequired().HasMaxLength(150);
                e.HasIndex(p => p.value).IsUnique();
            });

            builder.Property(x => x.HashedPassword).IsRequired();
            builder.Property(x => x.RoleId);
            builder.Property(x => x.LastLoginAt);
        }
    }
}
