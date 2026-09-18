using Application.Abstractions.Repositories;
using Application.Dto;
using Domain.Aggregates.Monitoring.StorageStatus;
using MediatR;
using SharedKernel.Shared;

namespace Application.Features.StorageStatus.Queries.GetStorageStatusByType
{
    public class GetStorageStatusByTypeQueryHandler : IRequestHandler<GetStorageStatusByTypeQuery, ResultT<StorageStatusDto>>
    {
        private readonly IStorageStatusRepository _storageStatusRepository;

        public GetStorageStatusByTypeQueryHandler(IStorageStatusRepository storageStatusRepository)
        {
            _storageStatusRepository = storageStatusRepository;
        }

        public async Task<ResultT<StorageStatusDto>> Handle(GetStorageStatusByTypeQuery request, CancellationToken cancellationToken)
        {
            var status = await _storageStatusRepository.GetByStorageTypeAsync(request.storageType, cancellationToken);
            if (status is null)
                return StorageStatusErrors.NotFound(request.storageType);

            return new StorageStatusDto(status.Id, status.StorageType, status.UsedGb, status.CapacityGb, status.PercentUsed, status.LastCheckedAt);
        }
    }
}