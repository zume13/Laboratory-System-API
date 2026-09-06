using MediatR;
using SharedKernel.Shared;

namespace Application.Features.LabOrder.Commands.CreateLabOrder
{
    public record CreateLabOrderCommand(Guid AppointmentId) : IRequest<ResultT<Guid>>;
}
