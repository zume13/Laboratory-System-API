using Application.Dto;
using MediatR;
using SharedKernel.Shared;

namespace Application.Features.LaboratoryRequests.Queries.GetLaboratoryRequestsAwaitingResults
{
    // staff's work queue
    public record GetLaboratoryRequestsAwaitingResultsQuery() : IRequest<ResultT<List<LaboratoryRequestDto>>>;
}