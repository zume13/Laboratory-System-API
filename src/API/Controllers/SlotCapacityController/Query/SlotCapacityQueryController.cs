using Application.Dto;
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
    public class SlotCapacityQueryController : ControllerBase
    {
        private readonly IMediator _mediator;

        public SlotCapacityQueryController(IMediator mediator)
        {
            _mediator = mediator;
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