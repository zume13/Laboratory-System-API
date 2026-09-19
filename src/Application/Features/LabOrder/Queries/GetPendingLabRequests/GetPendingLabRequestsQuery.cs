using Application.Features.LabOrder.Dto;
using MediatR;
using SharedKernel.Shared;

namespace Application.Features.LabOrder.Queries.GetPendingLabRequests
{
    public record GetPendingLabRequestsQuery() : IRequest<ResultT<List<PendingLabRequestDto>>>;
}