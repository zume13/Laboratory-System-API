using Application.Dto;
using Application.Features.TestCategories.Queries.GetActiveTestCategories;
using Application.Features.TestCategories.Queries.GetAllTestCategories;
using Application.Features.TestCategories.GetTestCategoryById;

using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SharedKernel.Constants;
using Microsoft.AspNetCore.RateLimiting;

namespace Laboratory_Management_API.Controllers.TestCategoryController
{
    [Route("api/[controller]")]
    [ApiController]
    public class TestCategoryQueryController : ControllerBase
    {
        private readonly IMediator _mediator;
        public TestCategoryQueryController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [EnableRateLimiting(SystemConstants.RateLimits.perUser)]
        [Authorize(Policy = SystemConstants.AuthPolicies.companyPersonnel)]
        [HttpGet("get-all-categories")]
        public async Task<IActionResult> GetAll()
        {
            var result = await _mediator.Send(new GetAllTestCategoriesQuery());

            if (result.IsFailure)
                return BadRequest(result.Error);

            return Ok(result.value);
        }

        [EnableRateLimiting(SystemConstants.RateLimits.anonymous)]
        [AllowAnonymous]
        [HttpGet("get-active-categories")]
        public async Task<IActionResult> GetActive()
        {
            var result = await _mediator.Send(new GetActiveTestCategoriesQuery());

            if (result.IsFailure)
                return BadRequest(result.Error);

            return Ok(result.value);
        }


        [EnableRateLimiting(SystemConstants.RateLimits.perUser)]
        [Authorize(Policy = SystemConstants.AuthPolicies.companyPersonnel)]
        [HttpGet("getBy/{id}")]
        public async Task<IActionResult> Get(Guid id)
        {
            var result = await _mediator.Send(new GetTestCategoryByIdQuery(id));

            if (result.IsFailure)
                return BadRequest(result.Error);

            return Ok(result.value);
        }
    }
}