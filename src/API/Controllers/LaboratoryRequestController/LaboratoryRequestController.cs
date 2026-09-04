using MediatR;
using Microsoft.AspNetCore.Mvc;


namespace Laboratory_Management_API.Controllers.LaboratoryRequestController
{
    [Route("api/[controller]")]
    [ApiController]
    public class LaboratoryRequestController : ControllerBase
    {
        private readonly IMediator _mediator;

        public LaboratoryRequestController(IMediator mediator)
        {
            _mediator = mediator;
        }
    }
}