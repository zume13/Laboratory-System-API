using Application.Dto;
using MediatR;
using SharedKernel.Shared;

namespace Application.Features.StorageStatus.Queries.GetStorageStatusByType
{
    public record GetStorageStatusByTypeQuery(string storageType) : IRequest<ResultT<StorageStatusDto>>;
}