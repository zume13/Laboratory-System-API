using MediatR;
using SharedKernel.Shared;

namespace Application.Features.LabOrder.CreateLabOrder
{
    public record CreateLabOrderCommand(Guid AppointmentId) : IRequest<ResultT<Guid>>;
}
