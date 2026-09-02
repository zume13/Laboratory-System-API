using Application.Dto;
using MediatR;
using SharedKernel.Shared;

namespace Application.Features.LaboratoryRequests.Queries.GetLaboratoryRequestById
{
    // view one request in detail
    public record GetLaboratoryRequestByIdQuery(Guid laboratoryRequestId) : IRequest<ResultT<LaboratoryRequestDto>>;
}