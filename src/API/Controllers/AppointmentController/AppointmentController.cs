using Application.Features.Appointments.Commands.AddAppointmentTest;
using Application.Features.Appointments.Commands.ApproveAppointmentTest;
using Application.Features.Appointments.Commands.CancelAppointment;
using Application.Features.Appointments.Commands.CancelAppointmentTest;
using Application.Features.Appointments.Commands.CreateOnlineAppointment;
using Application.Features.Appointments.Commands.CreateWalkInAppointment;
using Application.Features.Appointments.Commands.MarkAppointmentNoShow;
using Application.Features.Appointments.Commands.RemoveAppointmentTest;
using Application.Features.Appointments.Commands.RescheduleAppointment;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.RateLimiting;
using SharedKernel.Constants;

namespace Laboratory_Management_API.Controllers.AppointmentController
{
    [ApiController]
    [Route("api/[controller]")]
    public class AppointmentController : ControllerBase
    {
        private readonly IMediator _mediator;

        public AppointmentController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [EnableRateLimiting(SystemConstants.RateLimits.perUser)]
        [Authorize(Policy = SystemConstants.AuthPolicies.companyPersonnel)]
        [HttpPost("online")]
        public async Task<IActionResult> CreateOnlineAppointment(
            CreateOnlineAppointmentCommand command)
        {
            var result = await _mediator.Send(command);

            if (result.IsFailure)
                return BadRequest(result.Error);

            return Ok(result.value);
        }

        [EnableRateLimiting(SystemConstants.RateLimits.perUser)]
        [Authorize(Policy = SystemConstants.AuthPolicies.companyPersonnel)]
        [HttpPost("walk-in")]
        public async Task<IActionResult> CreateWalkInAppointment(
            CreateWalkInAppointmentCommand command)
        {
            var result = await _mediator.Send(command);

            if (result.IsFailure)
                return BadRequest(result.Error);

            return Ok(result.value);
        }

        [EnableRateLimiting(SystemConstants.RateLimits.perUser)]
        [Authorize(Policy = SystemConstants.AuthPolicies.companyPersonnel)]
        [HttpPost("{appointmentId}/cancel")]
        public async Task<IActionResult> CancelAppointment(CancelAppointmentCommand command)
        {
            var result = await _mediator.Send(command);

            if (result.IsFailure)
                return BadRequest(result.Error);

            return Ok();
        }

        [EnableRateLimiting(SystemConstants.RateLimits.perUser)]
        [Authorize(Policy = SystemConstants.AuthPolicies.companyPersonnel)]
        [HttpPut("{appointmentId}/reschedule")]
        public async Task<IActionResult> RescheduleAppointment(RescheduleAppointmentCommand command)
        {
            var result = await _mediator.Send(command);

            if (result.IsFailure)
                return BadRequest(result.Error);

            return Ok();
        }

        [EnableRateLimiting(SystemConstants.RateLimits.perUser)]
        [Authorize(Policy = SystemConstants.AuthPolicies.companyPersonnel)]
        [HttpPost("{appointmentId}/tests")]
        public async Task<IActionResult> AddTest(AddAppointmentTestCommand command)
        {
            var result = await _mediator.Send(command);

            if (result.IsFailure)
                return BadRequest(result.Error);

            return Ok();
        }

        [EnableRateLimiting(SystemConstants.RateLimits.perUser)]
        [Authorize(Policy = SystemConstants.AuthPolicies.companyPersonnel)]
        [HttpDelete("{appointmentId}/tests/{appointmentTestId}")]
        public async Task<IActionResult> RemoveTest(RemoveAppointmentTestCommand command)
        {
            var result = await _mediator.Send(command);

            if (result.IsFailure)
                return BadRequest(result.Error);

            return NoContent();
        }

        [EnableRateLimiting(SystemConstants.RateLimits.perUser)]
        [Authorize(Policy = SystemConstants.AuthPolicies.companyPersonnel)]
        [HttpPost("{appointmentId}/tests/{appointmentTestId}/approve")]
        public async Task<IActionResult> ApproveAppointmentTest(AproveAppointmentTestCommand command)
        {
            var result = await _mediator.Send(command);

            if (result.IsFailure)
                return BadRequest(result.Error);

            return Ok();
        }

        [EnableRateLimiting(SystemConstants.RateLimits.perUser)]
        [Authorize(Policy = SystemConstants.AuthPolicies.companyPersonnel)]
        [HttpPost("{appointmentId}/tests/{appointmentTestId}/cancel")]
        public async Task<IActionResult> CancelAppointmentTest(CancelAppointmentTestCommand command)
        {
            var result = await _mediator.Send(command);

            if (result.IsFailure)
                return BadRequest(result.Error);

            return Ok();
        }

        [EnableRateLimiting(SystemConstants.RateLimits.perUser)]
        [Authorize(Policy = SystemConstants.AuthPolicies.companyPersonnel)]
        [HttpPost("{appointmentId}/no-show")]
        public async Task<IActionResult> MarkNoShow(MarkAppointmentNoShowCommand command)
        {
            var result = await _mediator.Send(command);

            if (result.IsFailure)
                return BadRequest(result.Error);

            return Ok();
        }
    }
}