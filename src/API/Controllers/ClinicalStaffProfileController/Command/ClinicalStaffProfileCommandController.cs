using Application.Features.ClinicalStaffProfile.Commands.ChangeStaffRole;
using Application.Features.ClinicalStaffProfile.Commands.DeactivateStaffProfile;
using Application.Features.ClinicalStaffProfile.Commands.ReactivateStaffProfile;
using Domain.Aggregates.Identity.UserProfile.Enums;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.RateLimiting;
using SharedKernel.Constants;

namespace Laboratory_Management_API.Controllers.ClinicalStaffProfileController.Command
{
    [Route("api/[controller]")]
    [ApiController]
    public class ClinicalStaffProfileQueryController : ControllerBase
    {
        private readonly IMediator _mediator;

        public ClinicalStaffProfileQueryController(IMediator mediator)
        {
            _mediator = mediator;
        }


        [EnableRateLimiting(SystemConstants.RateLimits.perUser)]
        [Authorize(Policy = SystemConstants.AuthPolicies.adminOnly)]
        [HttpPut("{userId}/change-role")]
        public async Task<IActionResult> ChangeRole(Guid userId, [FromBody] StaffRole newRole)
        {
            var result = await _mediator.Send(new ChangeStaffRoleCommand(userId, newRole));
            if (result.IsFailure) return BadRequest(result.Error);
            return Ok();
        }

        [EnableRateLimiting(SystemConstants.RateLimits.perUser)]
        [Authorize(Policy = SystemConstants.AuthPolicies.adminOnly)]
        [HttpPost("{userId}/deactivate-profile")]
        public async Task<IActionResult> Deactivate(Guid userId)
        {
            var result = await _mediator.Send(new DeactivateStaffProfileCommand(userId));
            if (result.IsFailure) return BadRequest(result.Error);
            return Ok();
        }

        [EnableRateLimiting(SystemConstants.RateLimits.perUser)]
        [Authorize(Policy = SystemConstants.AuthPolicies.adminOnly)]
        [HttpPost("{userId}/reactivate-profile")]
        public async Task<IActionResult> Reactivate(Guid userId)
        {
            var result = await _mediator.Send(new ReactivateStaffProfileCommand(userId));
            if (result.IsFailure) return BadRequest(result.Error);
            return Ok();
        }

    }
}