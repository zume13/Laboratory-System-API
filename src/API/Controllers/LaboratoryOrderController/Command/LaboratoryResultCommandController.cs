using Application.Features.LabOrder.Commands.RemoveLabResult;
using Application.Features.LabOrder.Commands.UploadLaboratoryResult;
using Laboratory_Management_API.Models;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.RateLimiting;
using SharedKernel.Constants;
using System.Security.Claims;

namespace Laboratory_Management_API.Controllers.LaboratoryOrderController.Command
{
    [ApiController]
    [Route("api/[controller]")]
    public class LaboratoryResultController : ControllerBase
    {
        private readonly IMediator _mediator;

        public LaboratoryResultController(IMediator mediator)
        {
            _mediator = mediator;
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

        [EnableRateLimiting(SystemConstants.RateLimits.perUser)]
        [Authorize(Policy = SystemConstants.AuthPolicies.companyPersonnel)]
        [HttpDelete("{labOrderId}/requests/{requestId}/result")] 
        public async Task<IActionResult> RemoveLabResult(Guid labOrderId, Guid requestId, CancellationToken cancellationToken) 
        { 
            var result = await _mediator.Send(new RemoveLabResultCommand(labOrderId, requestId), cancellationToken); 
            if (result.IsFailure) 
                return BadRequest(result.Error); 
            
            return NoContent(); 
        }
    }
}