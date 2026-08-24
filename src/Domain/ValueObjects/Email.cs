using LeaveManagement.SharedKernel.Primitives;
using SharedKernel.Shared;

namespace Domain.ValueObjects
{
    public sealed class Email : ValueObject
    {
        public string value { get; }

        private Email(string value) => this.value = value;

        public static ResultT<Email> Create(string value)
        {
            if (string.IsNullOrWhiteSpace(value))
                return GeneralErrors.General.Empty(nameof(value));

            if (!value.Contains('@') || !value.Contains('.'))
                return GeneralErrors.General.Invalid(nameof(value));

            return new Email(value.Trim().ToLowerInvariant());
        }

        public override IEnumerable<object> GetAtomicValues()
        {
            yield return value;
        }
    }
}
