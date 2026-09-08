using MediatR;
using Microsoft.AspNetCore.Mvc;


namespace Laboratory_Management_API.Controllers.ClinicalStaffProfileController.Command
{
    [Route("api/[controller]")]
    [ApiController]
    public class ClinicalStaffProfileQueryController : ControllerBase
    {
        private readonly IMediator _mediator;

        public ClinicalStaffProfileQueryController(IMediator mediator)
        {
            _mediator = mediator;
        }
    }
}