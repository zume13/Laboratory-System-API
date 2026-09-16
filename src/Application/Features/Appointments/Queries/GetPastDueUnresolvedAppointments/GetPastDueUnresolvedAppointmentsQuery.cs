using Application.Dto;
using MediatR;
using SharedKernel.Shared;

namespace Application.Features.Appointments.Queries.GetPastDueUnresolvedAppointments
{
    public record GetPastDueUnresolvedAppointmentsQuery(DateTime asOf) : IRequest<ResultT<List<AppointmentDto>>>;
}