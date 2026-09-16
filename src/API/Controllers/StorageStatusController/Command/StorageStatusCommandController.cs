using Application.Dto;
using Application.Features.StorageStatus.Command.CreateStorageStatusRecord;
using Application.Features.StorageStatus.Command.UpdateStorageUsage;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.RateLimiting;
using SharedKernel.Constants;

namespace Laboratory_Management_API.Controllers.StorageStatusController
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize(Policy = SystemConstants.AuthPolicies.adminOnly)]
    public class StorageStatusCommandController : ControllerBase
    {
        private readonly IMediator _mediator;

        public StorageStatusCommandController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [EnableRateLimiting(SystemConstants.RateLimits.perUser)]
        [HttpPost("create")]
        public async Task<IActionResult> Create(CreateStorageStatusRecordCommand command)
        {
            var result = await _mediator.Send(command);
            if (result.IsFailure) return BadRequest(result.Error);
            return Ok(result.value);
        }

        [EnableRateLimiting(SystemConstants.RateLimits.perUser)]
        [HttpPut("update-usage")]
        public async Task<IActionResult> UpdateUsage(UpdateStorageUsageCommand command)
        {
            var result = await _mediator.Send(command);
            if (result.IsFailure) return BadRequest(result.Error);
            return NoContent();
        }

    }
}