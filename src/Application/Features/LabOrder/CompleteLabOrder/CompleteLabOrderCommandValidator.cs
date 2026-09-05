using FluentValidation;

namespace Application.Features.LabOrder.CompleteLabOrder
{
    internal class CompleteLabOrderCommandValidator : AbstractValidator<CompleteLabOrderCommand>
    {
        public CompleteLabOrderCommandValidator()
        {
            RuleFor(x => x.LabOrderId)
                .NotEmpty().WithMessage("LabOrderId is required.");
        }
    }
}
