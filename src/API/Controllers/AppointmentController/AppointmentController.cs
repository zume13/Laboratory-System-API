using Application.Dto;
using Application.Features.AppointmentTests.Commands.AddTestToAppointment;
using Application.Features.AppointmentTests.Commands.RemoveTestFromAppointment;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.RateLimiting;
using SharedKernel.Constants;

namespace Laboratory_Management_API.Controllers.AppointmentController
{
    public class AppointmentController : ControllerBase
    {
        private readonly IMediator _mediator;
        public AppointmentController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [EnableRateLimiting(SystemConstants.RateLimits.perUser)]
        [Authorize(Policy = SystemConstants.AuthPolicies.companyPersonnel)]
        [HttpPost("{id}/add-test")]
        public async Task<IActionResult> AddTest(Guid id, AddTestToAppointmentDto dto)
        {
            var command = new AddTestToAppointmentCommand(id, dto.testCategoryId);

            var result = await _mediator.Send(command);
            if (result.IsFailure) return BadRequest(result.Error);
            return Ok(result.value);
        }

        [EnableRateLimiting(SystemConstants.RateLimits.perUser)]
        [Authorize(Policy = SystemConstants.AuthPolicies.companyPersonnel)]
        [HttpDelete("{id}/remove-test/{appointmentTestId}")]
        public async Task<IActionResult> RemoveTest(Guid id, Guid appointmentTestId)
        {
            var command = new RemoveTestFromAppointmentCommand(id, appointmentTestId);

            var result = await _mediator.Send(command);
            if (result.IsFailure) return BadRequest(result.Error);
            return Ok();
        }
    }
}