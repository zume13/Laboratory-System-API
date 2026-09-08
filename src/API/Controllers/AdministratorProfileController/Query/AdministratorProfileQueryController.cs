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
    public class AdministratorProfileQueryController : ControllerBase
    {
        private readonly IMediator _mediator;

        public AdministratorProfileQueryController(IMediator mediator)
        {
            _mediator = mediator;
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