using Application.Features.LabOrder.Queries.GetAllLabOrdersByPatientId;
using Application.Features.LabOrder.Queries.GetLabOrderByPatientId;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.RateLimiting;
using SharedKernel.Constants;

namespace Laboratory_Management_API.Controllers.LaboratoryRequestOrderController
{
    [ApiController]
    [Route("api/[controller]")]
    public class LaboratoryRequestOrderQueryController : ControllerBase
    {
        private readonly IMediator _mediator;

        public LaboratoryRequestOrderQueryController(IMediator mediator)
        {
            _mediator = mediator;
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
    }
}