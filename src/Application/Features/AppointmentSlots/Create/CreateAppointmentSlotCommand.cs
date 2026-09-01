using MediatR;
using SharedKernel.Shared;
namespace Application.Features.AppointmentSlots.Create
{
    public record CreateAppointmentSlotCommand(
            DateTime date,
            TimeSpan startTime,
            TimeSpan endTime,
            Guid? testCategoryId,
            int capacity,
            Guid configuredByStaffId)
            : IRequest<ResultT<Guid>>;
}
