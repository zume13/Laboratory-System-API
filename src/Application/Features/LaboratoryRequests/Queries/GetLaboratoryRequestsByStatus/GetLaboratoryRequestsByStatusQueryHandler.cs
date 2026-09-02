using Application.Abstractions.Repositories;
using Application.Dto;
using MediatR;
using SharedKernel.Shared;

namespace Application.Features.LaboratoryRequests.Queries.GetLaboratoryRequestsByStatus
{
    public class GetLaboratoryRequestsByStatusQueryHandler : IRequestHandler<GetLaboratoryRequestsByStatusQuery, ResultT<List<LaboratoryRequestDto>>>
    {
        private readonly ILaboratoryRequestRepository _laboratoryRequestRepository;

        public GetLaboratoryRequestsByStatusQueryHandler(ILaboratoryRequestRepository laboratoryRequestRepository)
        {
            _laboratoryRequestRepository = laboratoryRequestRepository;
        }

        public async Task<ResultT<List<LaboratoryRequestDto>>> Handle(GetLaboratoryRequestsByStatusQuery request, CancellationToken cancellationToken)
        {
            var requests = await _laboratoryRequestRepository.GetByStatusAsync(request.status, cancellationToken);
            return requests.Select(LaboratoryRequestMapper.ToDto).ToList();
        }
    }
}