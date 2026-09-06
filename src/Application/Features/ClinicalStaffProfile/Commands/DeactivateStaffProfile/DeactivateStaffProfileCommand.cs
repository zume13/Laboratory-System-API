using MediatR;
using SharedKernel.Shared;

namespace Application.Features.ClinicalStaffProfile.Commands.DeactivateStaffProfile
{
    public record DeactivateStaffProfileCommand(Guid staffUserId) : IRequest<Result>;
}