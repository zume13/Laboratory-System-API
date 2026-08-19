using Application.Abstractions.Repositories;
using Domain.Aggregates.Laboratory.TestCategory;
using Infrastructure.Persistence.Database;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Persistence.Repositories
{
    public class TestCategoryRepository : Repository<TestCategory>, ITestCategoryRepository
    {
        public TestCategoryRepository(ApplicationDbContext dbContext) : base(dbContext) { }
        public async Task<TestCategory?> GetByNameAsync(string name, CancellationToken cancellationToken = default)
        {
            return await _dbContext.TestCategories
                .FirstOrDefaultAsync(t => t.Name.value == name, cancellationToken);
        }

        public async Task<List<TestCategory>> GetActiveAsync(CancellationToken cancellationToken = default)
        {
            return await _dbContext.TestCategories
                .Where(t => t.IsActive)
                .ToListAsync(cancellationToken);
        }
    }
}
