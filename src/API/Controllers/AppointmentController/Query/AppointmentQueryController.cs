using Application.Features.Appointments.Commands.AddAppointmentTest;
using Application.Features.Appointments.Commands.ApproveAppointmentTest;
using Application.Features.Appointments.Commands.CancelAppointment;
using Application.Features.Appointments.Commands.CancelAppointmentTest;
using Application.Features.Appointments.Commands.CreateOnlineAppointment;
using Application.Features.Appointments.Commands.CreateWalkInAppointment;
using Application.Features.Appointments.Commands.MarkAppointmentNoShow;
using Application.Features.Appointments.Commands.RemoveAppointmentTest;
using Application.Features.Appointments.Commands.RescheduleAppointment;
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

    }
}