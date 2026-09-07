using Application.Features.AdministratorProfile.Commands.PromoteUserToAdmin;
using Application.Features.AdministratorProfile.Commands.UpdateAdministratorPermissions;
using Application.Features.AdministratorProfile.Queries.GetAdministratorProfileByUserId;
using Application.Features.AdministratorProfile.Queries.GetActivityLogsByDate;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.RateLimiting;
using SharedKernel.Constants;

namespace Laboratory_Management_API.Controllers.AdministratorProfileController
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize(Policy = SystemConstants.AuthPolicies.adminOnly)]
    public class AdministratorProfileController : ControllerBase
    {
        private readonly IMediator _mediator;

        public AdministratorProfileController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [EnableRateLimiting(SystemConstants.RateLimits.perUser)]
        [HttpPost("{userId}/promote")]
        public async Task<IActionResult> Promote(Guid userId, [FromBody] string permissions)
        {
            var result = await _mediator.Send(new PromoteUserToAdminCommand(userId, permissions));
            if (result.IsFailure) return BadRequest(result.Error);
            return Ok(result.value);
        }

        [EnableRateLimiting(SystemConstants.RateLimits.perUser)]
        [HttpPut("{userId}/permissions")]
        public async Task<IActionResult> UpdatePermissions(Guid userId, [FromBody] string permissions)
        {
            var result = await _mediator.Send(new UpdateAdministratorPermissionsCommand(userId, permissions));
            if (result.IsFailure) return BadRequest(result.Error);
            return Ok();
        }

        [EnableRateLimiting(SystemConstants.RateLimits.perUser)]
        [HttpGet("{userId}")]
        public async Task<IActionResult> GetByUserId(Guid userId)
        {
            var result = await _mediator.Send(new GetAdministratorProfileByUserIdQuery(userId));
            if (result.IsFailure) return BadRequest(result.Error);
            return Ok(result.value);
        }

        [EnableRateLimiting(SystemConstants.RateLimits.perUser)]
        [HttpGet("activity-logs/{date}")]
        public async Task<IActionResult> GetActivityLogsByDate(DateTime date)
        {
            var result = await _mediator.Send(new GetActivityLogsByDateQuery(date));
            if (result.IsFailure) return BadRequest(result.Error);
            return Ok(result.value);
        }
    }
}