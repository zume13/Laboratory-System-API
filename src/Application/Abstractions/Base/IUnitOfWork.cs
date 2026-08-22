
using SharedKernel.Shared;

namespace Application.Abstractions.Base
{
    public interface IUnitOfWork
    {
        Task<Result> SaveChangesAsync(
            CancellationToken cancellationToken = default);
    }
}
