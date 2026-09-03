using Application.Dto;
using Application.Features.SlotCapacity.Commands.Create;
using Application.Features.SlotCapacity.Commands.Update;
using Application.Features.SlotCapacity.Queries.GetSlotCapacityConfigByTestCategoryId;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.RateLimiting;
using SharedKernel.Constants;

namespace Laboratory_Management_API.Controllers.SlotCapacityController
{
    [Route("api/[controller]")]
    [ApiController]
    public class SlotCapacityController : ControllerBase
    {
        private readonly IMediator _mediator;

        public SlotCapacityController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [EnableRateLimiting(SystemConstants.RateLimits.perUser)]
        [Authorize(Policy = SystemConstants.AuthPolicies.adminOnly)]
        [HttpPost("create-slot-capacity-config")]
        public async Task<IActionResult> Create(CreateSlotCapacityConfigDto dto)
        {
            var command = new CreateSlotCapacityConfigCommand(dto.testCategoryId, dto.maxDailyBookings, dto.maxPerSlot);

            var result = await _mediator.Send(command);
            if (result.IsFailure) return BadRequest(result.Error);
            return Ok(result.value);
        }

        [EnableRateLimiting(SystemConstants.RateLimits.perUser)]
        [Authorize(Policy = SystemConstants.AuthPolicies.adminOnly)]
        [HttpPut("update/{testCategoryId}")]
        public async Task<IActionResult> UpdateLimits(Guid testCategoryId, UpdateSlotCapacityLimitsDto dto)
        {
            var command = new UpdateSlotCapacityLimitsCommand(testCategoryId, dto.maxDailyBookings, dto.maxPerSlot);

            var result = await _mediator.Send(command);
            if (result.IsFailure) return BadRequest(result.Error);
            return Ok();
        }

        [EnableRateLimiting(SystemConstants.RateLimits.perUser)]
        [Authorize(Policy = SystemConstants.AuthPolicies.companyPersonnel)]
        [HttpGet("get/{testCategoryId}")]
        public async Task<IActionResult> GetByTestCategory(Guid testCategoryId)
        {
            var result = await _mediator.Send(new GetSlotCapacityConfigByTestCategoryIdQuery(testCategoryId));
            if (result.IsFailure) return BadRequest(result.Error);
            return Ok(result.value);
        }
    }
}