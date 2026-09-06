using Application.Dto;
using MediatR;
using SharedKernel.Shared;

namespace Application.Features.AdministratorProfile.Queries.GetAdministratorProfileByUserId
{
    public record GetAdministratorProfileByUserIdQuery(Guid userId) : IRequest<ResultT<AdministratorProfileDto>>;
}