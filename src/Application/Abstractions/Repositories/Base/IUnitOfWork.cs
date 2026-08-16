using SharedKernel.Shared;

namespace Application.Abstractions.Repositories.Base
{
    public interface IUnitOfWork
    {
        Task SaveChangesAsync(
            CancellationToken cancellationToken = default);
    }
}
