using MediatR;
using SharedKernel.Shared;

namespace Application.Features.AdministratorProfile.Commands.UpdateAdministratorPermissions
{
    public record UpdateAdministratorPermissionsCommand(Guid userId, string permissions) : IRequest<Result>;
}