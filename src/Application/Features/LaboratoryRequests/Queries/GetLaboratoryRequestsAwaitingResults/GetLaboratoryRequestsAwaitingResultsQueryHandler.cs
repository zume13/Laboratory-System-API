using Application.Abstractions.Repositories;
using Application.Dto;
using MediatR;
using SharedKernel.Shared;

namespace Application.Features.LaboratoryRequests.Queries.GetLaboratoryRequestsAwaitingResults
{
    public class GetLaboratoryRequestsAwaitingResultsQueryHandler : IRequestHandler<GetLaboratoryRequestsAwaitingResultsQuery, ResultT<List<LaboratoryRequestDto>>>
    {
        private readonly ILaboratoryRequestRepository _laboratoryRequestRepository;

        public GetLaboratoryRequestsAwaitingResultsQueryHandler(ILaboratoryRequestRepository laboratoryRequestRepository)
        {
            _laboratoryRequestRepository = laboratoryRequestRepository;
        }

        public async Task<ResultT<List<LaboratoryRequestDto>>> Handle(GetLaboratoryRequestsAwaitingResultsQuery request, CancellationToken cancellationToken)
        {
            var requests = await _laboratoryRequestRepository.GetPendingWithoutResultAsync(cancellationToken);
            return requests.Select(LaboratoryRequestMapper.ToDto).ToList();
        }
    }
}