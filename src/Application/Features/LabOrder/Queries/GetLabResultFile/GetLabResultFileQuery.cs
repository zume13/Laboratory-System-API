using MediatR;
using SharedKernel.Shared;

namespace Application.Features.LabOrder.Queries.GetLabResultFile
{
    public record GetLabResultFileQuery(string relativePath) : IRequest<ResultT<GetLabResultFileDto>>;
}
