using Application.Features.Appointments.Queries.GetAppointmentWithTests;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.RateLimiting;
using SharedKernel.Constants;

namespace Laboratory_Management_API.Controllers.AppointmentController.Command
{
    [ApiController]
    [Route("api/[controller]")]
    public class AppointmentQueryController : ControllerBase
    {
        private readonly IMediator _mediator;

        public AppointmentQueryController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [EnableRateLimiting(SystemConstants.RateLimits.perUser)]
        [Authorize(Policy = SystemConstants.AuthPolicies.companyPersonnel)]
        [HttpGet("get-tests/{appointmentId}")]
        public async Task<IActionResult> GetById(Guid appointmentId)
        {
            var result = await _mediator.Send(new GetAppointmentWithTestsQuery(appointmentId));
            if (result.IsFailure) return BadRequest(result.Error);
            return Ok(result.value);
        }
    }
}