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
    public class PatientProfileQueryController : ControllerBase
    {
        private readonly IMediator _mediator;

        public PatientProfileQueryController(IMediator mediator)
        {
            _mediator = mediator;
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

        [EnableRateLimiting(SystemConstants.RateLimits.perUser)]
        [Authorize(Policy = SystemConstants.AuthPolicies.patients)]
        [HttpGet("my-lab-requests")]
        public async Task<IActionResult> GetMyLabRequests()
        {
            var patientUserId = Guid.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier)!);
            var result = await _mediator.Send(new GetAllLaboratoryRequestByIdQuery(patientUserId));
            if (result.IsFailure) return BadRequest(result.Error);
            return Ok(result.value);
        }
    }
}