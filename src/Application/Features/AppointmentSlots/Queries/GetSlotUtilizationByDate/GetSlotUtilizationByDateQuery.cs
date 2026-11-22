using Application.Dto;
using MediatR;
using SharedKernel.Shared;

namespace Application.Features.AppointmentSlots.Queries.GetSlotUtilizationByDate
{
    public record GetSlotUtilizationByDateQuery(DateTime date) : IRequest<ResultT<List<SlotUtilizationDto>>>;
}