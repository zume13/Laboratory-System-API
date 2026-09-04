using Application.Dto;
using MediatR;
using SharedKernel.Shared;

namespace Application.Features.LaboratoryRequests.Queries.GetLaboratoryRequestsByStatus
{
    // staff filtering
    public record GetLaboratoryRequestsByStatusQuery(RequestStatus status) : IRequest<ResultT<List<LaboratoryRequestDto>>>;
}