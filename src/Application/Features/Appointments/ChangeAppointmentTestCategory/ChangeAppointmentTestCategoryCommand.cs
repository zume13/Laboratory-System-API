using MediatR;
using SharedKernel.Shared;

namespace Application.Features.Appointments.ChangeAppointmentTestCategory
{
    public record ChangeAppointmentTestCategoryCommand(Guid AppointmentId, Guid NewTestCategoryId) : IRequest<Result>;
}
