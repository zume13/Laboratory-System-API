using MediatR;
using SharedKernel.Shared;

namespace Application.Features.ClinicalStaffProfile.Commands.ReactivateStaffProfile
{
    public record ReactivateStaffProfileCommand(Guid staffUserId) : IRequest<Result>;
}