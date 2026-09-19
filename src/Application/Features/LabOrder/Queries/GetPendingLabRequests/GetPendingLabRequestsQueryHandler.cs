using Application.Abstractions.Repositories;
using Application.Features.LabOrder.Dto;
using MediatR;
using SharedKernel.Shared;

namespace Application.Features.LabOrder.Queries.GetPendingLabRequests
{
    public class GetPendingLabRequestsQueryHandler : IRequestHandler<GetPendingLabRequestsQuery, ResultT<List<PendingLabRequestDto>>>
    {
        private readonly ILabOrderRepository _labOrderRepository;

        public GetPendingLabRequestsQueryHandler(ILabOrderRepository labOrderRepository)
        {
            _labOrderRepository = labOrderRepository;
        }

        public async Task<ResultT<List<PendingLabRequestDto>>> Handle(GetPendingLabRequestsQuery request, CancellationToken cancellationToken)
        {
            var pendingRequests = await _labOrderRepository.GetPendingLabRequestsAsync(cancellationToken);
            return pendingRequests;
        }
    }
}