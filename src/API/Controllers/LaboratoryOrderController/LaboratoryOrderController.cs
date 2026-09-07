using Application.Features.LabOrder.Commands.AddLabRequest;
using Application.Features.LabOrder.Commands.CancelLabOrder;
using Application.Features.LabOrder.Commands.CompleteLabOrder;
using Application.Features.LabOrder.Commands.CreateLabOrder;
using Application.Features.LabOrder.Commands.RemoveLabRequest;
using Application.Features.LabOrder.Commands.UploadLaboratoryResult;
using Application.Features.LabOrder.Queries.GetAllLabOrdersByPatientId;
using Application.Features.LabOrder.Queries.GetLabOrderByPatientId;
using Laboratory_Management_API.Models;
using Microsoft.AspNetCore.Http;
using System.Security.Claims;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.RateLimiting;
using SharedKernel.Constants;

namespace Laboratory_Management_API.Controllers.LaboratoryRequestOrderController
{
    [ApiController]
    [Route("api/[controller]")]
    public class LaboratoryRequestOrderController : ControllerBase
    {
        private readonly IMediator _mediator;

        public LaboratoryRequestOrderController(IMediator mediator)
        {
            _mediator = mediator;
        }


        [EnableRateLimiting(SystemConstants.RateLimits.perUser)]
        [Authorize(Policy = SystemConstants.AuthPolicies.companyPersonnel)]
        [HttpPost("create")]
        public async Task<IActionResult> CreateLabOrder(
            [FromBody] CreateLabOrderCommand command)
        {
            var result = await _mediator.Send(command);

            if (result.IsFailure)
                return BadRequest(result.Error);

            return Ok(result.value);
        }

        [EnableRateLimiting(SystemConstants.RateLimits.perUser)]
        [Authorize(Policy = SystemConstants.AuthPolicies.companyPersonnel)]
        [HttpPost("add-request")]
        public async Task<IActionResult> AddLabRequest(
            [FromBody] AddLabRequestCommand command)
        {
            var result = await _mediator.Send(command);

            if (result.IsFailure)
                return BadRequest(result.Error);

            return Ok();
        }

        [EnableRateLimiting(SystemConstants.RateLimits.perUser)]
        [Authorize(Policy = SystemConstants.AuthPolicies.companyPersonnel)]
        [HttpDelete("remove-request")]
        public async Task<IActionResult> RemoveLabRequest(
            [FromBody] RemoveLabRequestCommand command)
        {
            var result = await _mediator.Send(command);

            if (result.IsFailure)
                return BadRequest(result.Error);

            return NoContent();
        }

        [EnableRateLimiting(SystemConstants.RateLimits.perUser)]
        [Authorize(Policy = SystemConstants.AuthPolicies.companyPersonnel)]
        [HttpPost("complete")]
        public async Task<IActionResult> CompleteLabOrder(
            [FromBody] CompleteLabOrderCommand command)
        {
            var result = await _mediator.Send(command);

            if (result.IsFailure)
                return BadRequest(result.Error);

            return NoContent();
        }


        [EnableRateLimiting(SystemConstants.RateLimits.perUser)]
        [Authorize(Policy = SystemConstants.AuthPolicies.companyPersonnel)]
        [HttpPost("cancel")]
        public async Task<IActionResult> CancelLabOrder(
            [FromBody] CancelLabOrderCommand command)
        {
            var result = await _mediator.Send(command);

            if (result.IsFailure)
                return BadRequest(result.Error);

            return NoContent();
        }

        [EnableRateLimiting(SystemConstants.RateLimits.perUser)]
        [Authorize(Policy = SystemConstants.AuthPolicies.companyPersonnel)]
        [HttpGet("all-lab-orders-by/{patientId}")]
        public async Task<IActionResult> GetAllByPatient(Guid patientId)
        {
            var result = await _mediator.Send(new GetAllLabOrdersByPatientIdQuery(patientId));

            if (result.IsFailure)
                return BadRequest(result.Error);

            return Ok(result.value);
        }

        [EnableRateLimiting(SystemConstants.RateLimits.perUser)]
        [Authorize(Policy = SystemConstants.AuthPolicies.companyPersonnel)]
        [HttpGet("get/{labOrderId}")]
        public async Task<IActionResult> GetById(Guid labOrderId)
        {
            var result = await _mediator.Send(new GetPatientLabOrderQuery(labOrderId));

            if (result.IsFailure)
                return BadRequest(result.Error);

            return Ok(result.value);
        }

        [EnableRateLimiting(SystemConstants.RateLimits.perUser)]
        [Authorize(Policy = SystemConstants.AuthPolicies.companyPersonnel)]
        [HttpPost("{labOrderId}/requests/{requestId}/result")]
        public async Task<IActionResult> UploadResult(
            Guid labOrderId,
            Guid requestId,
            [FromForm] UploadLabResultForm form)
        {
            var uploadedByStaffId = Guid.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier)!);

            var command = new UploadLabResultCommand(
                labOrderId,
                requestId,
                uploadedByStaffId,
                form.File.OpenReadStream(),
                form.File.FileName,
                form.SubFolder,
                form.SampleId);

            var result = await _mediator.Send(command);

            if (result.IsFailure)
                return BadRequest(result.Error);

            return Ok(result.value);
        }
    }
}