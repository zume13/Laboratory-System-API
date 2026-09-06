using MediatR;
using SharedKernel.Shared;

namespace Application.Features.AdministratorProfile.Commands.PromoteUserToAdmin
{
    public record PromoteUserToAdminCommand(Guid userId, string permissions) : IRequest<ResultT<Guid>>;
}