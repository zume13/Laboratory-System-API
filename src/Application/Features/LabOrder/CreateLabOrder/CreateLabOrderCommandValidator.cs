using FluentValidation;

namespace Application.Features.LabOrder.CreateLabOrder
{
    public class CreateLabOrderCommandValidator : AbstractValidator<CreateLabOrderCommand>
    {
        public CreateLabOrderCommandValidator() 
        { 
            RuleFor(x => x.AppointmentId).NotEmpty().WithMessage("AppointmentId is required.");
        }
    }
}
