using Application.Dto;
using Application.Features.ClinicalStaffProfile.Commands.ChangeStaffRole;
using Application.Features.ClinicalStaffProfile.Commands.DeactivateStaffProfile;
using Application.Features.ClinicalStaffProfile.Commands.ReactivateStaffProfile;
using Application.Features.ClinicalStaffProfile.Queries.GetAllAppointmentsBySlot;
using Application.Features.ClinicalStaffProfile.Queries.GetActiveClinicalStaff;
using Application.Features.ClinicalStaffProfile.Queries.GetClinicalStaffByRole;
using Application.Features.ClinicalStaffProfile.Queries.GetStaffProfile;
using Domain.Aggregates.Identity.UserProfile.Enums;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.RateLimiting;
using SharedKernel.Constants;

namespace Laboratory_Management_API.Controllers.ClinicalStaffProfileController
{
    [Route("api/[controller]")]
    [ApiController]
    public class ClinicalStaffProfileQueryController : ControllerBase
    {
        private readonly IMediator _mediator;

        public ClinicalStaffProfileQueryController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [EnableRateLimiting(SystemConstants.RateLimits.perUser)]
        [Authorize(Policy = SystemConstants.AuthPolicies.adminOnly)]
        [HttpPut("{userId}/change-role")]
        public async Task<IActionResult> ChangeRole(Guid userId, [FromBody] StaffRole newRole)
        {
            var result = await _mediator.Send(new ChangeStaffRoleCommand(userId, newRole));
            if (result.IsFailure) return BadRequest(result.Error);
            return Ok();
        }

        [EnableRateLimiting(SystemConstants.RateLimits.perUser)]
        [Authorize(Policy = SystemConstants.AuthPolicies.adminOnly)]
        [HttpPost("{userId}/deactivate-profile")]
        public async Task<IActionResult> Deactivate(Guid userId)
        {
            var result = await _mediator.Send(new DeactivateStaffProfileCommand(userId));
            if (result.IsFailure) return BadRequest(result.Error);
            return Ok();
        }

        [EnableRateLimiting(SystemConstants.RateLimits.perUser)]
        [Authorize(Policy = SystemConstants.AuthPolicies.adminOnly)]
        [HttpPost("{userId}/reactivate-profile")]
        public async Task<IActionResult> Reactivate(Guid userId)
        {
            var result = await _mediator.Send(new ReactivateStaffProfileCommand(userId));
            if (result.IsFailure) return BadRequest(result.Error);
            return Ok();
        }

        [EnableRateLimiting(SystemConstants.RateLimits.perUser)]
        [Authorize(Policy = SystemConstants.AuthPolicies.companyPersonnel)]
        [HttpGet("get/{userId}")]
        public async Task<IActionResult> GetProfile(Guid userId)
        {
            var result = await _mediator.Send(new GetStaffProfileQuery(userId));
            if (result.IsFailure) return BadRequest(result.Error);
            return Ok(result.value);
        }

        [EnableRateLimiting(SystemConstants.RateLimits.perUser)]
        [Authorize(Policy = SystemConstants.AuthPolicies.companyPersonnel)]
        [HttpGet("by-role/{role}")]
        public async Task<IActionResult> GetByRole(StaffRole role)
        {
            var result = await _mediator.Send(new GetClinicalStaffByRoleQuery(role));
            if (result.IsFailure) return BadRequest(result.Error);
            return Ok(result.value);
        }

        [EnableRateLimiting(SystemConstants.RateLimits.perUser)]
        [Authorize(Policy = SystemConstants.AuthPolicies.companyPersonnel)]
        [HttpGet("get-active")]
        public async Task<IActionResult> GetActive()
        {
            var result = await _mediator.Send(new GetActiveClinicalStaffQuery());
            if (result.IsFailure) return BadRequest(result.Error);
            return Ok(result.value);
        }

        [EnableRateLimiting(SystemConstants.RateLimits.perUser)]
        [Authorize(Policy = SystemConstants.AuthPolicies.companyPersonnel)]
        [HttpGet("appointments/by-slot/{appointmentSlotId}")]
        public async Task<IActionResult> GetAppointmentsBySlot(Guid appointmentSlotId)
        {
            var result = await _mediator.Send(new GetAllAppointmentsBySlotQuery(appointmentSlotId));
            if (result.IsFailure) return BadRequest(result.Error);
            return Ok(result.value);
        }
    }
}