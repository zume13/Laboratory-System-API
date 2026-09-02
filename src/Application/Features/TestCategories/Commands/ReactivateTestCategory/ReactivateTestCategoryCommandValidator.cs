using FluentValidation;

namespace Application.Features.TestCategories.Commands.ReactivateTestCategory
{
    public class ReactivateTestCategoryCommandValidator : AbstractValidator<ReactivateTestCategoryCommand>
    {
        public ReactivateTestCategoryCommandValidator()
        {
            RuleFor(s => s.testCategoryId)
                .NotEmpty().WithMessage("Test category id is required.");
        }
    }
}