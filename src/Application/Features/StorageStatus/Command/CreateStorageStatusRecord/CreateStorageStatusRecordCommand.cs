using MediatR;
using SharedKernel.Shared;

namespace Application.Features.StorageStatus.Command.CreateStorageStatusRecord
{
    public record CreateStorageStatusRecordCommand(string storageType, decimal capacityGb) : IRequest<ResultT<Guid>>;
}