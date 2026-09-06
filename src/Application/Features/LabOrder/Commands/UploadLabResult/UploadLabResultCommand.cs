using MediatR;
using SharedKernel.Shared;

namespace Application.Features.LabOrder.Commands.UploadLaboratoryResult
{
    public sealed record UploadLabResultCommand(
        Guid labOrderId,
        Guid requestId,
        Guid uploadedByStaffId,
        Stream fileStream,
        string fileName,
        string subFolder,
        string sampleId) : IRequest<ResultT<Guid>>;
}