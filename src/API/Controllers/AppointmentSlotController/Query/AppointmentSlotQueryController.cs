using Application.Dto;
using Application.Features.AppointmentSlots.Commands.Create;
using Application.Features.AppointmentSlots.Commands.Delete;
using Application.Features.AppointmentSlots.Commands.Update;
using Application.Features.AppointmentSlots.Queries.GetAppointmentSlotById;
using Application.Features.AppointmentSlots.Queries.GetAppointmentSlotsByDate;
using Application.Features.AppointmentSlots.Queries.GetAppointmentSlotsByDateRange;
using Application.Features.AppointmentSlots.Queries.GetAvailableAppointmentSlots;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.RateLimiting;
using SharedKernel.Constants;
using System.Security.Claims;

namespace Laboratory_Management_API.Controllers.AppointmentSlotController
{
    [Route("api/[controller]")]
    [ApiController]
    public class AppointmentSlotQueryController : ControllerBase
    {
        private readonly IMediator _mediator;

        public AppointmentSlotQueryController(IMediator mediator)
        {
            _mediator = mediator;
        }
    }
}