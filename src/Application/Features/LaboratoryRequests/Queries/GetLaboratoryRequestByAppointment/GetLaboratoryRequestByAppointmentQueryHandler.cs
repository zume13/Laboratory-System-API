using Application.Abstractions.Repositories;
using Application.Dto;
using Domain.Aggregates.LaboratoryOrder.LaboratoryRequest;
using MediatR;
using SharedKernel.Shared;

namespace Application.Features.LaboratoryRequests.Queries.GetLaboratoryRequestByAppointment
{
    public class GetLaboratoryRequestByAppointmentQueryHandler : IRequestHandler<GetLaboratoryRequestByAppointmentQuery, ResultT<LaboratoryRequestDto>>
    {
        private readonly ILaboratoryRequestRepository _laboratoryRequestRepository;

        public GetLaboratoryRequestByAppointmentQueryHandler(ILaboratoryRequestRepository laboratoryRequestRepository)
        {
            _laboratoryRequestRepository = laboratoryRequestRepository;
        }

        public async Task<ResultT<LaboratoryRequestDto>> Handle(GetLaboratoryRequestByAppointmentQuery request, CancellationToken cancellationToken)
        {
            var labRequest = await _laboratoryRequestRepository.GetByAppointmentIdAsync(request.appointmentId, cancellationToken);
            if (labRequest is null)
                return LaboratoryRequestErrors.LaboratoryRequest.NotFound(request.appointmentId);

            return LaboratoryRequestMapper.ToDto(labRequest);
        }
    }
}