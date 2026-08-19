using Application.Abstractions.Base;
using Domain.Aggregates.Communications.SmsGateway;

namespace Application.Abstractions.Repositories
{
    public interface ISmsGatewayRepository : IRepository<SmsGatewayStatus>
    {
        Task<SmsGatewayStatus?> GetCurrentAsync(CancellationToken cancellationToken = default);
    }
}
