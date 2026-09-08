using Application.Features.LabOrder.Commands.AddLabRequest;
using Application.Features.LabOrder.Commands.CompleteLabRequest;
using Application.Features.LabOrder.Commands.ReleaseLabRequest;
using Application.Features.LabOrder.Commands.RemoveLabRequest;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.RateLimiting;
using SharedKernel.Constants;

namespace Laboratory_Management_API.Controllers.LaboratoryOrderController.Command
{
    [ApiController]
    [Route("api/[controller]")]
    public class LaboratoryRequestCommandController : ControllerBase
    {
        private readonly IMediator _mediator;

        public LaboratoryRequestCommandController(IMediator mediator)
        {
            _mediator = mediator;
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
        [HttpPost("release-request")]
        public async Task<IActionResult> ReleaseRequest([FromBody] ReleaseLabRequestCommand command)
        {
            var result = await _mediator.Send(command);

            if (result.IsFailure) 
                return BadRequest(result.Error);

            return Ok();
        }

        [EnableRateLimiting(SystemConstants.RateLimits.perUser)]
        [Authorize(Policy = SystemConstants.AuthPolicies.companyPersonnel)]
        [HttpPost("complete-request")]
        public async Task<IActionResult> CompleteRequest([FromBody] CompleteLabRequestCommand command)
        {
            var result = await _mediator.Send(command);

            if (result.IsFailure) 
                return BadRequest(result.Error);

            return Ok();
        }
    }
}