using Domain.Aggregates.Laboratory.TestCategory;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Persistence.EntityConfiguration
{
    public class TestCategoryConfig : IEntityTypeConfiguration<TestCategory>
    {
        public void Configure(EntityTypeBuilder<TestCategory> builder)
        {
            builder.ToTable("TestCategories");
            builder.HasKey(x => x.Id);

            builder.OwnsOne(x => x.Name, n =>
                n.Property(p => p.value).HasColumnName("Name").IsRequired().HasMaxLength(100));

            builder.OwnsOne(x => x.Price, p =>
                p.Property(m => m.value).HasColumnName("Price").HasColumnType("decimal(10,2)").IsRequired());

            builder.Property(x => x.IsActive).IsRequired();
        }
    }
}
