using SharedKernel.Shared;

namespace Application.Abstractions.Base
{
    public interface IUnitOfWork
    {
        Task SaveChangesAsync(
            CancellationToken cancellationToken = default);
    }
}
