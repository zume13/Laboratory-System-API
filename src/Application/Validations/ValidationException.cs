using FluentValidation.Results;
using SharedKernel.Shared;

namespace Application.Validations
{
    public sealed class ValidationException : Exception
    {
        public IReadOnlyList<Error> Errors { get; }

        public ValidationException(IEnumerable<ValidationFailure> failures)
        {
            Errors = failures
                .Select(f => Error.Validation(f.PropertyName, f.ErrorMessage))
                .ToList();
        }
    }
}