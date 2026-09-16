using Application.Dto;
using MediatR;
using SharedKernel.Shared;

namespace Application.Features.SystemConfig.Queries.GetSystemConfigByKey
{
    public record GetSystemConfigByKeyQuery(string key) : IRequest<ResultT<SystemConfigDto>>;
}