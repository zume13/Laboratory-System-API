using Application.Dto;
using Application.Features.PatientProfile.Commands.AcceptPatientConsent;
using Application.Features.PatientProfile.Commands.LinkPatientPhysicalRecord;
using Application.Features.PatientProfile.Queries.GetMyPatientProfile;
using Application.Features.PatientProfile.Queries.GetPatientProfileByPhysicalId;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.RateLimiting;
using SharedKernel.Constants;
using System.Security.Claims;

namespace Laboratory_Management_API.Controllers.PatientProfileController
{
    [Route("api/[controller]")]
    [ApiController]
    public class PatientProfileController : ControllerBase
    {
        private readonly IMediator _mediator;

        public PatientProfileController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [EnableRateLimiting(SystemConstants.RateLimits.perUser)]
        [Authorize(Policy = SystemConstants.AuthPolicies.patients)]
        [HttpPost("give-consent")]
        public async Task<IActionResult> AcceptConsent()
        {
            var patientUserId = Guid.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier)!);
            var result = await _mediator.Send(new AcceptPatientConsentCommand(patientUserId));
            if (result.IsFailure) return BadRequest(result.Error);
            return Ok();
        }

        [EnableRateLimiting(SystemConstants.RateLimits.perUser)]
        [Authorize(Policy = SystemConstants.AuthPolicies.companyPersonnel)]
        [HttpPost("{userId}/link-physical-record")]
        public async Task<IActionResult> LinkPhysicalRecord(Guid userId, [FromBody] string physicalPatientId)
        {
            var result = await _mediator.Send(new LinkPatientPhysicalRecordCommand(userId, physicalPatientId));
            if (result.IsFailure) return BadRequest(result.Error);
            return Ok();
        }

        [EnableRateLimiting(SystemConstants.RateLimits.perUser)]
        [Authorize(Policy = SystemConstants.AuthPolicies.patients)]
        [HttpGet("get-mine")] // sana all may mine haha...
        public async Task<IActionResult> GetMine()
        {
            var patientUserId = Guid.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier)!);
            var result = await _mediator.Send(new GetMyPatientProfileQuery(patientUserId));
            if (result.IsFailure) return BadRequest(result.Error);
            return Ok(result.value);
        }

        [EnableRateLimiting(SystemConstants.RateLimits.perUser)]
        [Authorize(Policy = SystemConstants.AuthPolicies.companyPersonnel)]
        [HttpGet("by-physical-id/{physicalPatientId}")]
        public async Task<IActionResult> GetByPhysicalId(string physicalPatientId)
        {
            var result = await _mediator.Send(new GetPatientProfileByPhysicalIdQuery(physicalPatientId));
            if (result.IsFailure) return BadRequest(result.Error);
            return Ok(result.value);
        }
    }
}