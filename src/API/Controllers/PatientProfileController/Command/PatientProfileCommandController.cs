using Application.Features.PatientProfile.Commands.AcceptPatientConsent;
using Application.Features.PatientProfile.Commands.LinkPatientPhysicalRecord;
using Application.Features.PatientProfile.Queries.GetMyPatientProfile;
using Application.Features.PatientProfile.Queries.GetPatientProfileByPhysicalId;
using Application.Features.PatientProfile.Queries.GetAllLaboratoryRequestById;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.RateLimiting;
using SharedKernel.Constants;
using System.Security.Claims;

namespace Laboratory_Management_API.Controllers.PatientProfileController.Command
{
    [Route("api/[controller]")]
    [ApiController]
    public class PatientProfileCommandController : ControllerBase
    {
        private readonly IMediator _mediator;

        public PatientProfileCommandController(IMediator mediator)
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
    }
}