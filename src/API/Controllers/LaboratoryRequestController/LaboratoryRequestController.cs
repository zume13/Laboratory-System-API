using Application.Dto;
using Application.Features.LaboratoryRequests.Commands.AttachPatientToWalkInRequest;
using Application.Features.LaboratoryRequests.Commands.CreateLaboratoryRequestForPatient;
using Application.Features.LaboratoryRequests.Commands.CreateLaboratoryRequestForWalkIn;
using Application.Features.LaboratoryRequests.Queries.GetLaboratoryRequestByAppointment;
using Application.Features.LaboratoryRequests.Queries.GetLaboratoryRequestById;
using Application.Features.LaboratoryRequests.Queries.GetLaboratoryRequestsAwaitingResults;
using Application.Features.LaboratoryRequests.Queries.GetLaboratoryRequestsByStatus;
using Application.Features.LaboratoryRequests.Queries.GetMyLaboratoryRequests;
using Application.Features.LaboratoryRequests.Queries.GetUnlinkedWalkInRequests;
using Application.Features.LaboratoryRequests.Commands.ReleaseLaboratoryResult;
using Application.Features.LaboratoryRequests.Commands.VoidLaboratoryRequest;
using Domain.Aggregates.Laboratory.LaboratoryRequest;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.RateLimiting;
using SharedKernel.Constants;
using System.Security.Claims;

namespace Laboratory_Management_API.Controllers.LaboratoryRequestController
{
    [Route("api/[controller]")]
    [ApiController]
    public class LaboratoryRequestController : ControllerBase
    {
        private readonly IMediator _mediator;

        public LaboratoryRequestController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [EnableRateLimiting(SystemConstants.RateLimits.perUser)]
        [Authorize(Policy = SystemConstants.AuthPolicies.companyPersonnel)]
        [HttpPost("create-for-patient")]
        public async Task<IActionResult> CreateForPatient(CreateLaboratoryRequestForPatientDto dto)
        {
            var command = new CreateLaboratoryRequestForPatientCommand(
                dto.patientId, dto.testCategoryId, dto.clinicalDetails, dto.appointmentId);

            var result = await _mediator.Send(command);
            if (result.IsFailure) return BadRequest(result.Error);
            return Ok(result.value);
        }

        [EnableRateLimiting(SystemConstants.RateLimits.perUser)]
        [Authorize(Policy = SystemConstants.AuthPolicies.companyPersonnel)]
        [HttpPost("create-for-walk-in")]
        public async Task<IActionResult> CreateForWalkIn(CreateLaboratoryRequestForWalkInDto dto)
        {
            var command = new CreateLaboratoryRequestForWalkInCommand(
                dto.physicalPatientId, dto.testCategoryId, dto.clinicalDetails);

            var result = await _mediator.Send(command);
            if (result.IsFailure) return BadRequest(result.Error);
            return Ok(result.value);
        }

        [EnableRateLimiting(SystemConstants.RateLimits.perUser)]
        [Authorize(Policy = SystemConstants.AuthPolicies.companyPersonnel)]
        [HttpPost("{id}/attach-patient-to-walkin/{patientId}")]
        public async Task<IActionResult> AttachPatient(Guid id, Guid patientId)
        {
            var result = await _mediator.Send(new AttachPatientToWalkInRequestCommand(id, patientId));
            if (result.IsFailure) return BadRequest(result.Error);
            return Ok();
        }

        [EnableRateLimiting(SystemConstants.RateLimits.perUser)]
        [Authorize(Policy = SystemConstants.AuthPolicies.companyPersonnel)]
        [HttpPost("{id}/release-result")]
        public async Task<IActionResult> Release(Guid id)
        {
            var result = await _mediator.Send(new ReleaseLaboratoryResultCommand(id));
            if (result.IsFailure) return BadRequest(result.Error);
            return Ok();
        }

        [EnableRateLimiting(SystemConstants.RateLimits.perUser)]
        [Authorize(Policy = SystemConstants.AuthPolicies.companyPersonnel)]
        [HttpPost("{id}/void-request")]
        public async Task<IActionResult> Void(Guid id)
        {
            var result = await _mediator.Send(new VoidLaboratoryRequestCommand(id));
            if (result.IsFailure) return BadRequest(result.Error);
            return Ok();
        }

        [EnableRateLimiting(SystemConstants.RateLimits.perUser)]
        [Authorize(Policy = SystemConstants.AuthPolicies.companyPersonnel)]
        [HttpGet("get-laboratory-request/{id}")]
        public async Task<IActionResult> GetById(Guid id)
        {
            var result = await _mediator.Send(new GetLaboratoryRequestByIdQuery(id));
            if (result.IsFailure) return BadRequest(result.Error);
            return Ok(result.value);
        }

        [EnableRateLimiting(SystemConstants.RateLimits.perUser)]
        [Authorize(Policy = SystemConstants.AuthPolicies.patients)]
        [HttpGet("get-my-laboratory-requests")]
        public async Task<IActionResult> GetMine()
        {
            var patientId = Guid.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier)!);
            var result = await _mediator.Send(new GetMyLaboratoryRequestsQuery(patientId));
            if (result.IsFailure) return BadRequest(result.Error);
            return Ok(result.value);
        }

        [EnableRateLimiting(SystemConstants.RateLimits.perUser)]
        [Authorize(Policy = SystemConstants.AuthPolicies.companyPersonnel)]
        [HttpGet("get-laboratory-request-by-appointment/{appointmentId}")]
        public async Task<IActionResult> GetByAppointment(Guid appointmentId)
        {
            var result = await _mediator.Send(new GetLaboratoryRequestByAppointmentQuery(appointmentId));
            if (result.IsFailure) return BadRequest(result.Error);
            return Ok(result.value);
        }

        [EnableRateLimiting(SystemConstants.RateLimits.perUser)]
        [Authorize(Policy = SystemConstants.AuthPolicies.companyPersonnel)]
        [HttpGet("get-by-status/{status}")]
        public async Task<IActionResult> GetByStatus(RequestStatus status)
        {
            var result = await _mediator.Send(new GetLaboratoryRequestsByStatusQuery(status));
            if (result.IsFailure) return BadRequest(result.Error);
            return Ok(result.value);
        }

        [EnableRateLimiting(SystemConstants.RateLimits.perUser)]
        [Authorize(Policy = SystemConstants.AuthPolicies.companyPersonnel)]
        [HttpGet("get-awaiting-results")]
        public async Task<IActionResult> GetAwaitingResults()
        {
            var result = await _mediator.Send(new GetLaboratoryRequestsAwaitingResultsQuery());
            if (result.IsFailure) return BadRequest(result.Error);
            return Ok(result.value);
        }

        [EnableRateLimiting(SystemConstants.RateLimits.perUser)]
        [Authorize(Policy = SystemConstants.AuthPolicies.companyPersonnel)]
        [HttpGet("get-unlinked/{physicalPatientId}")]
        public async Task<IActionResult> GetUnlinked(string physicalPatientId)
        {
            var result = await _mediator.Send(new GetUnlinkedWalkInRequestsQuery(physicalPatientId));
            if (result.IsFailure) return BadRequest(result.Error);
            return Ok(result.value);
        }
    }
}