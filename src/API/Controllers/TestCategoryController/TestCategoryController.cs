using Application.Dto;
using Application.TestCategories.CreateTestCategory;
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
    }
}