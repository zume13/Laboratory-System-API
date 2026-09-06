using Application.Features.LabOrder.Dto;
using MediatR;
using SharedKernel.Shared;

namespace Application.Features.LabOrder.Queries.GetLabOrderByPatientId
{
    public record GetPatientLabOrderQuery(Guid labOrderId) : IRequest<ResultT<LabOrderWithRequestDto>>;
}
