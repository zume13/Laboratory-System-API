using Application.Features.LabOrder.Commands.CancelLabOrder;
using Application.Features.LabOrder.Commands.CompleteLabOrder;
using Application.Features.LabOrder.Commands.CreateLabOrder;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.RateLimiting;
using SharedKernel.Constants;

namespace Laboratory_Management_API.Controllers.LaboratoryOrderController.Command
{
    [ApiController]
    [Route("api/[controller]")]
    public class LaboratoryRequestOrderCommandController : ControllerBase
    {
        private readonly IMediator _mediator;

        public LaboratoryRequestOrderCommandController(IMediator mediator)
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