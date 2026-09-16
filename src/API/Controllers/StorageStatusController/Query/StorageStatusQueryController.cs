using Application.Dto;
using Application.Features.StorageStatus.Queries.GetStorageStatusByType;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.RateLimiting;
using SharedKernel.Constants;

namespace Laboratory_Management_API.Controllers.StorageStatusQueryController
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize(Policy = SystemConstants.AuthPolicies.adminOnly)]
    public class StorageStatusQueryController : ControllerBase
    {
        private readonly IMediator _mediator;

        public StorageStatusQueryController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [EnableRateLimiting(SystemConstants.RateLimits.perUser)]
        [HttpGet("get-{storageType}")]
        public async Task<IActionResult> GetByType(string storageType)
        {
            var result = await _mediator.Send(new GetStorageStatusByTypeQuery(storageType));
            if (result.IsFailure) return BadRequest(result.Error);
            return Ok(result.value);
        }
    }
}