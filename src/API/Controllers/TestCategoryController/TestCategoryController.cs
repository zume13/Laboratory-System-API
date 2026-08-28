using Application.Dto;
using Application.TestCategories.CreateTestCategory;
using Application.TestCategories.UpdateTestCategoryPrice;
using Application.TestCategories.DeactivateTestCategory;
using Application.TestCategories.ReactivateTestCategory;
using Application.Users.LogIn;
using Application.Users.RegisterEmployee;
using Application.Users.RegisterPatient;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Laboratory_Management_API.Controllers.TestCategoryController
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize(Roles = "ClinicalStaff,Admin")]
    public class TestCategoryController : ControllerBase
    {
        private readonly IMediator _mediator;
        public TestCategoryController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpPost]
        public async Task<IActionResult> Create(CreateTestCategoryDto crtDto)
        {
            var command = new CreateTestCategoryCommand(crtDto.name, crtDto.price);
            var result = await _mediator.Send(command);
            if (result.IsFailure)
                return BadRequest(result.Error);
            return Ok();
        }

        [HttpPatch("{id}/updateprice")]
        public async Task<IActionResult> UpdatePrice(Guid id, UpdateTestCategoryPriceDto dto)
        {
            var command = new UpdateTestCategoryPriceCommand(id, dto.price);

            var result = await _mediator.Send(command);

            if (result.IsFailure)
                return BadRequest(result.Error);

            return Ok();
        }

        [HttpPost("{id}/deactivate")]
        public async Task<IActionResult> Deactivate(Guid id)
        {
            var command = new DeactivateTestCategoryCommand(id);

            var result = await _mediator.Send(command);

            if (result.IsFailure)
                return BadRequest(result.Error);

            return Ok();
        }

        [HttpPost("{id}/reactivate")]
        public async Task<IActionResult> Reactivate(Guid id)
        {
            var command = new ReactivateTestCategoryCommand(id);

            var result = await _mediator.Send(command);

            if (result.IsFailure)
                return BadRequest(result.Error);

            return Ok();
        }
    }
}