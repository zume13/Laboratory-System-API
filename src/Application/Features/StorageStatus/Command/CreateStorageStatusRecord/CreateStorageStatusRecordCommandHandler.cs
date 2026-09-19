using Application.Abstractions.Base;
using Application.Abstractions.Repositories;
using Domain.Aggregates.Monitoring.StorageStatus;
using MediatR;
using SharedKernel.Shared;

namespace Application.Features.StorageStatus.Command.CreateStorageStatusRecord
{
    public class CreateStorageStatusRecordCommandHandler : IRequestHandler<CreateStorageStatusRecordCommand, ResultT<Guid>>
    {
        private readonly IStorageStatusRepository _storageStatusRepository;
        private readonly IUnitOfWork _unitOfWork;

        public CreateStorageStatusRecordCommandHandler(IStorageStatusRepository storageStatusRepository, IUnitOfWork unitOfWork)
        {
            _storageStatusRepository = storageStatusRepository;
            _unitOfWork = unitOfWork;
        }

        public async Task<ResultT<Guid>> Handle(CreateStorageStatusRecordCommand request, CancellationToken cancellationToken)
        {
            var existing = await _storageStatusRepository.GetByStorageTypeAsync(request.storageType, cancellationToken);
            if (existing is not null)
                return StorageStatusErrors.AlreadyExists(request.storageType);

            var statusResult = Domain.Aggregates.Monitoring.StorageStatus.StorageStatus.Initialize(request.storageType, request.capacityGb);
            if (statusResult.IsFailure)
                return statusResult.Error;

            await _storageStatusRepository.AddAsync(statusResult.value, cancellationToken);

            var saveResult = await _unitOfWork.SaveChangesAsync(cancellationToken);
            if (saveResult.IsFailure)
                return saveResult.Error;

            return statusResult.value.Id;
        }
    }
}