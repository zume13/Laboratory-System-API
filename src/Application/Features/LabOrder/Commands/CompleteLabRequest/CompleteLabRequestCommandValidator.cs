using FluentValidation;

namespace Application.Features.LabOrder.Commands.CompleteLabRequest
{
    public class CompleteLabRequestCommandValidator : AbstractValidator<CompleteLabRequestCommand>
    {
        public CompleteLabRequestCommandValidator()
        {
            RuleFor(t => t.LabOrderId)
                .NotEmpty().WithMessage("LabOrderId is required.");
            RuleFor(w => w.RequestId)
                .NotEmpty().WithMessage("Request ID is required.");
        }
    }
}