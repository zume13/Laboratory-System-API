using Application.Features.LabOrder.Commands.RemoveLabResult;
using Application.Features.LabOrder.Commands.UploadLaboratoryResult;
using Application.Features.LabOrder.Queries.GetLabResultFile;
using Laboratory_Management_API.Models;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.RateLimiting;
using SharedKernel.Constants;
using System.Security.Claims;

namespace Laboratory_Management_API.Controllers.LaboratoryRequestOrderController
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
        [Authorize(Policy = SystemConstants.AuthPolicies.patients)]
        [HttpGet("result-file")] 
        public async Task<IActionResult> GetLabResultFile([FromQuery] string relativePath, CancellationToken cancellationToken) 
        { 
            var result = await _mediator.Send(new GetLabResultFileQuery(relativePath), cancellationToken); 

            if (result.IsFailure) 
                return BadRequest(result.Error); 
            
            return Ok(File(result.value.stream, result.value.contentType, result.value.fileName));
        }
    }
}