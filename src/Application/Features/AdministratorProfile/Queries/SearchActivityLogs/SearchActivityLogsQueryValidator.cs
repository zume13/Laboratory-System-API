using FluentValidation;

namespace Application.Features.AdministratorProfile.Queries.SearchActivityLogs
{
    public class SearchActivityLogsQueryValidator : AbstractValidator<SearchActivityLogsQuery>
    {
        public SearchActivityLogsQueryValidator()
        {
            RuleFor(x => x.page)
                .GreaterThanOrEqualTo(1).WithMessage("Page must be at least 1.");

            RuleFor(x => x.pageSize)
                .InclusiveBetween(1, 100).WithMessage("Page size must be between 1 and 100.");

            RuleFor(x => x.target)
                .MaximumLength(200).WithMessage("Target cannot exceed 200 characters.");

            RuleFor(x => x.to)
                .Must((query, to) => !query.from.HasValue || !to.HasValue || to.Value >= query.from.Value)
                .WithMessage("'to' must be on or after 'from'.");
        }
    }
}