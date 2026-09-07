using FluentValidation;

namespace Application.Features.LabOrder.Commands.RemoveLabResult
{
    internal class RemoveLabResultCommandValidator : AbstractValidator<RemoveLabResultCommand>
    {
        public RemoveLabResultCommandValidator()
        {
            RuleFor(x => x.requestId).NotEmpty().WithMessage("Request Id is required");
            RuleFor(x => x.labOrderId).NotEmpty().WithMessage("Lab order id Id is required");
        }
    }
}
