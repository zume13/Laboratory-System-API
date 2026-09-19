using Application.Abstractions.Base;
using Domain.Aggregates.Otp;

namespace Application.Abstractions.Repositories;

public interface IOtpRepository : IRepository<Otp>
{
    Task<Otp?> GetActiveByUserIdAsync(
        Guid userId,
        CancellationToken cancellationToken = default);

}