using FluentValidation;

namespace Application.Features.LabOrder.AddLabRequest
{
    public class AddLabRequestCommandValidator : AbstractValidator<AddLabRequestCommand>
    {
        public AddLabRequestCommandValidator()
        {
            RuleFor(x => x.LabOrderId)
                .NotEmpty().WithMessage("LabOrderId is required.");
            RuleFor(x => x.TestCategoryId)
                .NotEmpty().WithMessage("TestCategoryId is required.");
        }
        }
}
