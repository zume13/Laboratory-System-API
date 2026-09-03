using Application.Dto;
using MediatR;
using SharedKernel.Shared;

namespace Application.Features.AppointmentSlots.Queries.GetAppointmentSlotsByDate
{

    public record GetAppointmentSlotsByDateQuery(DateTime date) : IRequest<ResultT<List<AppointmentSlotDto>>>;
}