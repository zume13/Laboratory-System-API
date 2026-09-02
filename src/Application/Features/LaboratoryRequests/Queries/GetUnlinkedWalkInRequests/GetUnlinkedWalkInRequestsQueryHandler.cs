using Application.Abstractions.Repositories;
using Application.Dto;
using MediatR;
using SharedKernel.Shared;

namespace Application.Features.LaboratoryRequests.Queries.GetUnlinkedWalkInRequests
{
    public class GetUnlinkedWalkInRequestsQueryHandler : IRequestHandler<GetUnlinkedWalkInRequestsQuery, ResultT<List<LaboratoryRequestDto>>>
    {
        private readonly ILaboratoryRequestRepository _laboratoryRequestRepository;

        public GetUnlinkedWalkInRequestsQueryHandler(ILaboratoryRequestRepository laboratoryRequestRepository)
        {
            _laboratoryRequestRepository = laboratoryRequestRepository;
        }

        public async Task<ResultT<List<LaboratoryRequestDto>>> Handle(GetUnlinkedWalkInRequestsQuery request, CancellationToken cancellationToken)
        {
            var requests = await _laboratoryRequestRepository.GetUnlinkedByPhysicalPatientIdAsync(request.physicalPatientId, cancellationToken);
            return requests.Select(LaboratoryRequestMapper.ToDto).ToList();
        }
    }
}