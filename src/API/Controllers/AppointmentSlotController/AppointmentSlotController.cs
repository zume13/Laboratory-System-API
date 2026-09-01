using Application.Dto;
using Application.Features.AppointmentSlots.Create;
using Application.Features.AppointmentSlots.Delete;
using Application.Features.AppointmentSlots.Update;
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
    }
}