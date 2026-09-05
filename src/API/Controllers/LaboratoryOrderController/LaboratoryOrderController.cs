using Application.Features.LabOrder.AddLabRequest;
using Application.Features.LabOrder.CancelLabOrder;
using Application.Features.LabOrder.CompleteLabOrder;
using Application.Features.LabOrder.CreateLabOrder;
using Application.Features.LabOrder.RemoveLabRequest;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.RateLimiting;
using SharedKernel.Constants;

namespace Laboratory_Management_API.Controllers.LaboratoryRequestOrderController
{
    [ApiController]
    [Route("api/[controller]")]
    public class LaboratoryRequestOrderController : ControllerBase
    {
        private readonly IMediator _mediator;

        public LaboratoryRequestOrderController(IMediator mediator)
        {
            _mediator = mediator;
        }


        [EnableRateLimiting(SystemConstants.RateLimits.perUser)]
        [Authorize(Policy = SystemConstants.AuthPolicies.companyPersonnel)]
        [HttpPost("create")]
        public async Task<IActionResult> CreateLabOrder(
            [FromBody] CreateLabOrderCommand command)
        {
            var result = await _mediator.Send(command);

            if (result.IsFailure)
                return BadRequest(result.Error);

            return Ok(result.value);
        }

        [EnableRateLimiting(SystemConstants.RateLimits.perUser)]
        [Authorize(Policy = SystemConstants.AuthPolicies.companyPersonnel)]
        [HttpPost("add-request")]
        public async Task<IActionResult> AddLabRequest(
            [FromBody] AddLabRequestCommand command)
        {
            var result = await _mediator.Send(command);

            if (result.IsFailure)
                return BadRequest(result.Error);

            return Ok();
        }

        [EnableRateLimiting(SystemConstants.RateLimits.perUser)]
        [Authorize(Policy = SystemConstants.AuthPolicies.companyPersonnel)]
        [HttpDelete("remove-request")]
        public async Task<IActionResult> RemoveLabRequest(
            [FromBody] RemoveLabRequestCommand command)
        {
            var result = await _mediator.Send(command);

            if (result.IsFailure)
                return BadRequest(result.Error);

            return NoContent();
        }

        [EnableRateLimiting(SystemConstants.RateLimits.perUser)]
        [Authorize(Policy = SystemConstants.AuthPolicies.companyPersonnel)]
        [HttpPost("complete")]
        public async Task<IActionResult> CompleteLabOrder(
            [FromBody] CompleteLabOrderCommand command)
        {
            var result = await _mediator.Send(command);

            if (result.IsFailure)
                return BadRequest(result.Error);

            return NoContent();
        }


        [EnableRateLimiting(SystemConstants.RateLimits.perUser)]
        [Authorize(Policy = SystemConstants.AuthPolicies.companyPersonnel)]
        [HttpPost("cancel")]
        public async Task<IActionResult> CancelLabOrder(
            [FromBody] CancelLabOrderCommand command)
        {
            var result = await _mediator.Send(command);

            if (result.IsFailure)
                return BadRequest(result.Error);

            return NoContent();
        }
    }
}