using Application.Dto;
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
    public class AppointmentSlotQueryController : ControllerBase
    {
        private readonly IMediator _mediator;

        public AppointmentSlotQueryController(IMediator mediator)
        {
            _mediator = mediator;
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
            var result = await _mediator.Send(new GetAppointmentSlotsByDateRangeQuery(from, to)); -
            if (result.IsFailure) return BadRequest(result.Error);
            return Ok(result.value);
        }
    }
}