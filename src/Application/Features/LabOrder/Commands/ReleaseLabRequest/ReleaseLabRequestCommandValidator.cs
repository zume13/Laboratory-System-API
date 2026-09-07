using FluentValidation;

namespace Application.Features.LabOrder.Commands.ReleaseLabRequest
{
    public class ReleaseLabRequestCommandValidator : AbstractValidator<ReleaseLabRequestCommand>
    {
        public ReleaseLabRequestCommandValidator()
        {
            RuleFor(i => i.LabOrderId)
                .NotEmpty().WithMessage("LabOrderId is required.");
            RuleFor(n => n.RequestId)
                .NotEmpty().WithMessage("Request ID is required.");
        }
    }
}