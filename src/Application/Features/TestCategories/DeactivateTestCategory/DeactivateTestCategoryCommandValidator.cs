using FluentValidation;

namespace Application.Features.TestCategories.DeactivateTestCategory
{
    public class DeactivateTestCategoryCommandValidator : AbstractValidator<DeactivateTestCategoryCommand>
    {
        public DeactivateTestCategoryCommandValidator()
        {
            RuleFor(e => e.testCategoryId)
                .NotEmpty().WithMessage("Test category id is required to deactivate dat.");
        }
    }
}