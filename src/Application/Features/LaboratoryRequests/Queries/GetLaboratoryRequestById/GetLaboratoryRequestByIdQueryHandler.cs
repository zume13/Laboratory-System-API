using Application.Abstractions.Repositories;
using Application.Dto;
using Domain.Aggregates.LaboratoryOrder.LaboratoryRequest;
using MediatR;
using SharedKernel.Shared;

namespace Application.Features.LaboratoryRequests.Queries.GetLaboratoryRequestById
{
    public class GetLaboratoryRequestByIdQueryHandler : IRequestHandler<GetLaboratoryRequestByIdQuery, ResultT<LaboratoryRequestDto>>
    {
        private readonly ILaboratoryRequestRepository _laboratoryRequestRepository;

        public GetLaboratoryRequestByIdQueryHandler(ILaboratoryRequestRepository laboratoryRequestRepository)
        {
            _laboratoryRequestRepository = laboratoryRequestRepository;
        }

        public async Task<ResultT<LaboratoryRequestDto>> Handle(GetLaboratoryRequestByIdQuery request, CancellationToken cancellationToken)
        {
            var labRequest = await _laboratoryRequestRepository.GetByIdAsync(request.laboratoryRequestId, cancellationToken);
            if (labRequest is null)
                return LaboratoryRequestErrors.LaboratoryRequest.NotFound(request.laboratoryRequestId);

            return LaboratoryRequestMapper.ToDto(labRequest);
        }
    }
}