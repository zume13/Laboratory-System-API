using Application.Dto;
using MediatR;
using SharedKernel.Shared;

namespace Application.Features.LaboratoryRequests.Queries.GetLaboratoryRequestByAppointment
{
    // request tied to a specific booking appointment
    public record GetLaboratoryRequestByAppointmentQuery(Guid appointmentId) : IRequest<ResultT<LaboratoryRequestDto>>;
}