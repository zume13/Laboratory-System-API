using Application.Dto;
using MediatR;
using SharedKernel.Shared;

namespace Application.Features.LaboratoryRequests.Queries.GetMyLaboratoryRequests
{
    // get patients laboratory requests, to view request/result history
    public record GetMyLaboratoryRequestsQuery(Guid patientId) : IRequest<ResultT<List<LaboratoryRequestDto>>>;
}