using Application.Dto;
using Application.Features.TestCategories.CreateTestCategory;
using Application.Features.TestCategories.UpdateTestCategoryPrice;
using Application.Features.TestCategories.DeactivateTestCategory;
using Application.Features.TestCategories.ReactivateTestCategory;
using Application.Features.Users.CommandQueries.GetActiveTestCategories;
using Application.Features.Users.CommandQueries.GetAllTestCategories;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SharedKernel.Constants;
using Microsoft.AspNetCore.RateLimiting;

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
        [HttpPost]
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
        [HttpPatch("{id}/updateprice")]
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
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var result = await _mediator.Send(new GetAllTestCategoriesQuery());

            if (result.IsFailure)
                return BadRequest(result.Error);

            return Ok(result.value);
        }

        [EnableRateLimiting(SystemConstants.RateLimits.anonymous)]
        [AllowAnonymous]
        [HttpGet("active")]
        public async Task<IActionResult> GetActive()
        {
            var result = await _mediator.Send(new GetActiveTestCategoriesQuery());

            if (result.IsFailure)
                return BadRequest(result.Error);

            return Ok(result.value);
        }
    }
}