using Application.Dto;
using Application.Users.LogIn;
using Application.Users.RegisterEmployee;
using Application.Users.RegisterPatient;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Laboratory_Management_API.Controllers.UserController
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IMediator _mediator;
        public UserController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [AllowAnonymous]
        [HttpPost("auth/register-employee")]
        public async Task<IActionResult> RegisterEmployee(RegisterEmployeeDto registerDto)
        {
            var command = new RegisterEmployeeCommand(
                registerDto.firstName,
                registerDto.lastName,
                registerDto.email,
                registerDto.phoneNumber,
                registerDto.password,
                registerDto.staffRole);

            var result = await _mediator.Send(command);

            if(result.IsFailure)
                return BadRequest(result.Error);

            return Ok();
        }

        [AllowAnonymous]
        [HttpPost("auth/register-patient")]
        public async Task<IActionResult> RegisterPatient(RegisterPatientDto registerDto)
        {
            var command = new RegisterPatientCommand(
                registerDto.firstName,
                registerDto.lastName,
                registerDto.email,
                registerDto.phoneNumber,
                registerDto.dateOfBirth,
                registerDto.sex,
                registerDto.password,
                registerDto.consent);

            var result = await _mediator.Send(command);

            if(result.IsFailure)
                return BadRequest(result.Error);

            return Ok();
        }

        [AllowAnonymous]
        [HttpPost("auth/login")]
        public async Task<IActionResult> Login([FromBody] LoginDto loginDto)
        {
            var command = new LoginCommand(loginDto.email, loginDto.password);

            var result = await _mediator.Send(command);

            if(result.IsFailure)
                return BadRequest(result.Error);

            return Ok();
        }
    }
}
