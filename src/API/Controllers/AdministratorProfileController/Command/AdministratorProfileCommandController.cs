using Application.Features.AdministratorProfile.Commands.PromoteUserToAdmin;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.RateLimiting;
using SharedKernel.Constants;

namespace Laboratory_Management_API.Controllers.AdministratorProfileController.Command
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize(Policy = SystemConstants.AuthPolicies.adminOnly)]
    public class AdministratorProfileCommandController : ControllerBase
    {
        private readonly IMediator _mediator;

        public AdministratorProfileCommandController(IMediator mediator)
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
    }
}