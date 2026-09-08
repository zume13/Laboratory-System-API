using Application.Dto;
using MediatR;
using SharedKernel.Shared;

namespace Application.Features.Appointments.Queries.GetAppointmentWithTests
{
    public record GetAppointmentWithTestsQuery(Guid appointmentId) : IRequest<ResultT<AppointmentWithTestsDto>>;
}