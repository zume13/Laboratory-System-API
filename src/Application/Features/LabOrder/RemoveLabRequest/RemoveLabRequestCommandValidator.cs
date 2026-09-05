using FluentValidation;

namespace Application.Features.LabOrder.RemoveLabRequest
{
    internal class RemoveLabRequestCommandValidator : AbstractValidator<RemoveLabRequestCommand>
    {
        public RemoveLabRequestCommandValidator()
        {
            RuleFor(x => x.LabOrderId)
                .NotEmpty().WithMessage("LabOrderId is required.");
            RuleFor(x => x.TestCategory)
                .NotEmpty().WithMessage("TestCategory is required.");
        }   
    }
}
