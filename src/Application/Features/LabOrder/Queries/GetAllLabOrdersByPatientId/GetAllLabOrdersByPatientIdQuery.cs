using Application.Features.LabOrder.Dto;
using MediatR;
using SharedKernel.Shared;

namespace Application.Features.LabOrder.Queries.GetAllLabOrdersByPatientId
{
    public record GetAllLabOrdersByPatientIdQuery(Guid PatientId) : IRequest<ResultT<List<LabOrderDto>>>;
}
