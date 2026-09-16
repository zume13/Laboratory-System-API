using Application.Dto;
using Application.Features.SystemConfig.Queries.GetSystemConfigByKey;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.RateLimiting;
using SharedKernel.Constants;

namespace Laboratory_Management_API.Controllers.SystemConfigQueryController
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize(Policy = SystemConstants.AuthPolicies.adminOnly)]
    public class SystemConfigQueryController : ControllerBase
    {
        private readonly IMediator _mediator;

        public SystemConfigQueryController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [EnableRateLimiting(SystemConstants.RateLimits.perUser)]
        [HttpGet("{key}")]
        public async Task<IActionResult> GetByKey(string key)
        {
            var result = await _mediator.Send(new GetSystemConfigByKeyQuery(key));
            if (result.IsFailure) return BadRequest(result.Error);
            return Ok(result.value);
        }
    }
}