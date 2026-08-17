using Domain.Aggregates.Laboratory.TestCategory;
using Infrastructure.Persistence.Database;

namespace Infrastructure.Persistence.Repositories
{
    public class TestCategoryRepository : Repository<TestCategory>
    {
        public TestCategoryRepository(ApplicationDbContext dbContext) : base(dbContext)
        {
        }
    }
}
