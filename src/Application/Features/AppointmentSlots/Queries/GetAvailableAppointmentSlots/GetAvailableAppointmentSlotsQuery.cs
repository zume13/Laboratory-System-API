using Application.Dto;
using MediatR;
using SharedKernel.Shared;

namespace Application.Features.AppointmentSlots.Queries.GetAvailableAppointmentSlots
{
    public record GetAvailableAppointmentSlotsQuery(DateTime date, Guid testCategoryId) : IRequest<ResultT<List<PublicAppointmentSlotDto>>>;
}