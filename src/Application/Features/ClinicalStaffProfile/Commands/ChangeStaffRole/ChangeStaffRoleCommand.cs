// Commands/ChangeStaffRole/ChangeStaffRoleCommand.cs
using Domain.Aggregates.Identity.UserProfile.Enums;
using MediatR;
using SharedKernel.Shared;

namespace Application.Features.ClinicalStaffProfile.Commands.ChangeStaffRole
{
    public record ChangeStaffRoleCommand(Guid staffUserId, StaffRole newRole) : IRequest<Result>;
}