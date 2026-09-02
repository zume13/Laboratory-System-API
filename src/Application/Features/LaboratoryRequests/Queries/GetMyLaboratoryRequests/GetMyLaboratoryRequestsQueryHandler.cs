using Application.Abstractions.Repositories;
using Application.Dto;
using MediatR;
using SharedKernel.Shared;

namespace Application.Features.LaboratoryRequests.Queries.GetMyLaboratoryRequests
{
    public class GetMyLaboratoryRequestsQueryHandler : IRequestHandler<GetMyLaboratoryRequestsQuery, ResultT<List<LaboratoryRequestDto>>>
    {
        private readonly ILaboratoryRequestRepository _laboratoryRequestRepository;

        public GetMyLaboratoryRequestsQueryHandler(ILaboratoryRequestRepository laboratoryRequestRepository)
        {
            _laboratoryRequestRepository = laboratoryRequestRepository;
        }

        public async Task<ResultT<List<LaboratoryRequestDto>>> Handle(GetMyLaboratoryRequestsQuery request, CancellationToken cancellationToken)
        {
            var requests = await _laboratoryRequestRepository.GetByPatientIdAsync(request.patientId, cancellationToken);
            return requests.Select(LaboratoryRequestMapper.ToDto).ToList();
        }
    }
}