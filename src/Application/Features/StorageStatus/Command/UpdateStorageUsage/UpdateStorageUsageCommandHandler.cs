using Application.Abstractions.Base;
using Application.Abstractions.Repositories;
using Domain.Aggregates.Monitoring.StorageStatus;
using MediatR;
using SharedKernel.Shared;

namespace Application.Features.StorageStatus.Command.UpdateStorageUsage
{
    public class UpdateStorageUsageCommandHandler : IRequestHandler<UpdateStorageUsageCommand, Result>
    {
        private readonly IStorageStatusRepository _storageStatusRepository;
        private readonly IUnitOfWork _unitOfWork;

        public UpdateStorageUsageCommandHandler(IStorageStatusRepository storageStatusRepository, IUnitOfWork unitOfWork)
        {
            _storageStatusRepository = storageStatusRepository;
            _unitOfWork = unitOfWork;
        }

        public async Task<Result> Handle(UpdateStorageUsageCommand request, CancellationToken cancellationToken)
        {
            var status = await _storageStatusRepository.GetByStorageTypeAsync(request.storageType, cancellationToken);
            if (status is null)
                return StorageStatusErrors.NotFound(request.storageType);

            var updateResult = status.UpdateUsage(request.usedGb);
            if (updateResult.IsFailure)
                return updateResult.Error;

            _storageStatusRepository.Update(status);

            var saveResult = await _unitOfWork.SaveChangesAsync(cancellationToken);
            if (saveResult.IsFailure)
                return saveResult.Error;

            return Result.Success();
        }
    }
}