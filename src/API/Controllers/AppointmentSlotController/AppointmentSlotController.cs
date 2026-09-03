using Application.Dto;
using Application.Features.AppointmentSlots.Commands.Create;
using Application.Features.AppointmentSlots.Commands.Delete;
using Application.Features.AppointmentSlots.Commands.Update;
using Application.Features.AppointmentSlots.Queries.GetAppointmentSlotById;
using Application.Features.AppointmentSlots.Queries.GetAppointmentSlotsByDate;
using Application.Features.AppointmentSlots.Queries.GetAppointmentSlotsByDateRange;
using Application.Features.AppointmentSlots.Queries.GetAvailableAppointmentSlots;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.RateLimiting;
using SharedKernel.Constants;
using System.Security.Claims;

namespace Laboratory_Management_API.Controllers.AppointmentSlotController
{
    [Route("api/[controller]")]
    [ApiController]
    public class AppointmentSlotController : ControllerBase
    {
        private readonly IMediator _mediator;

        public AppointmentSlotController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [EnableRateLimiting(SystemConstants.RateLimits.perUser)]
        [Authorize(Policy = SystemConstants.AuthPolicies.companyPersonnel)]
        [HttpPost("create-slot")]
        public async Task<IActionResult> Create(CreateAppointmentSlotDto dto)
        {
            var staffId = Guid.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier)!);

            var command = new CreateAppointmentSlotCommand(
                dto.date, dto.startTime, dto.endTime, dto.testCategoryId, dto.capacity, staffId);

            var result = await _mediator.Send(command);

            if (result.IsFailure)
                return BadRequest(result.Error);

            return Ok(result.value);
        }

        [EnableRateLimiting(SystemConstants.RateLimits.perUser)]
        [Authorize(Policy = SystemConstants.AuthPolicies.companyPersonnel)]
        [HttpDelete("{id}/delete-slot")]
        public async Task<IActionResult> Delete(Guid id)
        {
            var command = new DeleteAppointmentSlotCommand(id);

            var result = await _mediator.Send(command);

            if (result.IsFailure)
                return BadRequest(result.Error);

            return Ok();
        }

        [EnableRateLimiting(SystemConstants.RateLimits.perUser)]
        [Authorize(Policy = SystemConstants.AuthPolicies.companyPersonnel)]
        [HttpPut("{id}/update-slot")]
        public async Task<IActionResult> Update(Guid id, UpdateAppointmentSlotDto dto)
        {
            var command = new UpdateAppointmentSlotCommand(
                id, dto.date, dto.startTime, dto.endTime, dto.testCategoryId, dto.capacity);

            var result = await _mediator.Send(command);

            if (result.IsFailure)
                return BadRequest(result.Error);

            return Ok();
        }

        [EnableRateLimiting(SystemConstants.RateLimits.perUser)]
        [Authorize(Policy = SystemConstants.AuthPolicies.companyPersonnel)]
        [HttpGet("get-appointmentslot/{id}")]
        public async Task<IActionResult> GetById(Guid id)
        {
            var result = await _mediator.Send(new GetAppointmentSlotByIdQuery(id));
            if (result.IsFailure) return BadRequest(result.Error);
            return Ok(result.value);
        }

        [EnableRateLimiting(SystemConstants.RateLimits.perUser)]
        [Authorize(Policy = SystemConstants.AuthPolicies.companyPersonnel)]
        [HttpGet("get-appointmentslot-by-date/{date}")]
        public async Task<IActionResult> GetByDate(DateTime date)
        {
            var result = await _mediator.Send(new GetAppointmentSlotsByDateQuery(date));
            if (result.IsFailure) return BadRequest(result.Error);
            return Ok(result.value);
        }

        [EnableRateLimiting(SystemConstants.RateLimits.anonymous)]
        [AllowAnonymous]
        [HttpGet("get-appointmentslot-available")]
        public async Task<IActionResult> GetAvailable([FromQuery] DateTime date, [FromQuery] Guid testCategoryId)
        {
            var result = await _mediator.Send(new GetAvailableAppointmentSlotsQuery(date, testCategoryId));
            if (result.IsFailure) return BadRequest(result.Error);
            return Ok(result.value);
        }

        [EnableRateLimiting(SystemConstants.RateLimits.perUser)]
        [Authorize(Policy = SystemConstants.AuthPolicies.companyPersonnel)]
        [HttpGet("get-appointmentslot-daterange")]
        public async Task<IActionResult> GetByDateRange([FromQuery] DateTime from, [FromQuery] DateTime to)
        {
            var result = await _mediator.Send(new GetAppointmentSlotsByDateRangeQuery(from, to));
            if (result.IsFailure) return BadRequest(result.Error);
            return Ok(result.value);
        }
    }
}