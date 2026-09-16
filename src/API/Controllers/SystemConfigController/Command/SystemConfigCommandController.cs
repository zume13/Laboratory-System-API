using Application.Dto;
using Application.Features.SystemConfig.Commands.CreateSystemConfig;
using Application.Features.SystemConfig.Commands.UpdateSystemConfigValue;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.RateLimiting;
using SharedKernel.Constants;

namespace Laboratory_Management_API.Controllers.SystemConfigController
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize(Policy = SystemConstants.AuthPolicies.adminOnly)]
    public class SystemConfigCommandController : ControllerBase
    {
        private readonly IMediator _mediator;

        public SystemConfigCommandController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [EnableRateLimiting(SystemConstants.RateLimits.perUser)]
        [HttpPost("create")]
        public async Task<IActionResult> Create(CreateSystemConfigCommand command)
        {
            var result = await _mediator.Send(command);
            if (result.IsFailure) return BadRequest(result.Error);
            return Ok(result.value);
        }

        [EnableRateLimiting(SystemConstants.RateLimits.perUser)]
        [HttpPut("update")]
        public async Task<IActionResult> Update(UpdateSystemConfigValueCommand command)
        {
            var result = await _mediator.Send(command);
            if (result.IsFailure) return BadRequest(result.Error);
            return NoContent();
        }
    }
}