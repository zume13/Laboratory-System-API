using Application.Dto;
using Application.Features.TestCategories.Commands.CreateTestCategory;
using Application.Features.TestCategories.Commands.UpdateTestCategoryPrice;
using Application.Features.TestCategories.Commands.DeactivateTestCategory;
using Application.Features.TestCategories.Commands.ReactivateTestCategory;
using Application.Features.TestCategories.Queries.GetActiveTestCategories;
using Application.Features.TestCategories.Queries.GetAllTestCategories;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SharedKernel.Constants;
using Microsoft.AspNetCore.RateLimiting;
using Application.Features.TestCategories.GetTestCategoryById;

namespace Laboratory_Management_API.Controllers.TestCategoryController
{
    [Route("api/[controller]")]
    [ApiController]
    public class TestCategoryController : ControllerBase
    {
        private readonly IMediator _mediator;
        public TestCategoryController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [EnableRateLimiting(SystemConstants.RateLimits.perUser)]
        [Authorize(Policy = SystemConstants.AuthPolicies.adminOnly)]
        [HttpPost("create-category")]
        public async Task<IActionResult> Create(CreateTestCategoryDto crtDto)
        {
            var command = new CreateTestCategoryCommand(crtDto.name, crtDto.price);
            var result = await _mediator.Send(command);
            if (result.IsFailure)
                return BadRequest(result.Error);
            return Ok();
        }

        [EnableRateLimiting(SystemConstants.RateLimits.perUser)]
        [Authorize(Policy = SystemConstants.AuthPolicies.adminOnly)]
        [HttpPatch("{id}/update-price")]
        public async Task<IActionResult> UpdatePrice(Guid id, UpdateTestCategoryPriceDto dto)
        {
            var command = new UpdateTestCategoryPriceCommand(id, dto.price);

            var result = await _mediator.Send(command);

            if (result.IsFailure)
                return BadRequest(result.Error);

            return Ok();
        }

        [EnableRateLimiting(SystemConstants.RateLimits.perUser)]
        [Authorize(Policy = SystemConstants.AuthPolicies.adminOnly)]
        [HttpPost("{id}/deactivate")]
        public async Task<IActionResult> Deactivate(Guid id)
        {
            var command = new DeactivateTestCategoryCommand(id);

            var result = await _mediator.Send(command);

            if (result.IsFailure)
                return BadRequest(result.Error);

            return Ok();
        }

        [EnableRateLimiting(SystemConstants.RateLimits.perUser)]
        [Authorize(Policy = SystemConstants.AuthPolicies.adminOnly)]
        [HttpPost("{id}/reactivate")]
        public async Task<IActionResult> Reactivate(Guid id)
        {
            var command = new ReactivateTestCategoryCommand(id);

            var result = await _mediator.Send(command);

            if (result.IsFailure)
                return BadRequest(result.Error);

            return Ok();
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