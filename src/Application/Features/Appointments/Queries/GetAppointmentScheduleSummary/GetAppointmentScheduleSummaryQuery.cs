using Application.Dto;
using MediatR;
using SharedKernel.Shared;

namespace Application.Features.Appointments.Queries.GetAppointmentScheduleSummary
{
    public record GetAppointmentScheduleSummaryQuery(DateTime date) : IRequest<ResultT<AppointmentScheduleSummaryDto>>;
}