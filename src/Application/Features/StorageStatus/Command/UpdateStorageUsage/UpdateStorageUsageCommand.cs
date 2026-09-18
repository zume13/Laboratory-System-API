using MediatR;
using SharedKernel.Shared;

namespace Application.Features.StorageStatus.Command.UpdateStorageUsage
{
    public record UpdateStorageUsageCommand(string storageType, decimal usedGb) : IRequest<Result>;
}