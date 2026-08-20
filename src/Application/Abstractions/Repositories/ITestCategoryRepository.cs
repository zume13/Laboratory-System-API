using Application.Abstractions.Base;
using Domain.Aggregates.Laboratory.TestCategory;

namespace Application.Abstractions.Repositories.Laboratory
{
    public interface ITestCategoryRepository : IRepository<TestCategory>
    {
        Task<TestCategory?> GetByNameAsync(string name, CancellationToken cancellationToken = default);
        Task<List<TestCategory>> GetActiveAsync(CancellationToken cancellationToken = default);
    }
}
