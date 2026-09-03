using Application.Dto;
using MediatR;
using SharedKernel.Shared;
namespace Application.Features.AppointmentSlots.Queries.GetAppointmentSlotById {
public record GetAppointmentSlotByIdQuery(Guid appointmentSlotId) : IRequest<ResultT<AppointmentSlotDto>>;
}
