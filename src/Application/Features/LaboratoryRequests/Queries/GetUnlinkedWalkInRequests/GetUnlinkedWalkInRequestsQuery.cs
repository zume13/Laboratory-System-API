using Application.Dto;
using MediatR;
using SharedKernel.Shared;

namespace Application.Features.LaboratoryRequests.Queries.GetUnlinkedWalkInRequests
{
    public record GetUnlinkedWalkInRequestsQuery(string physicalPatientId) : IRequest<ResultT<List<LaboratoryRequestDto>>>;
}