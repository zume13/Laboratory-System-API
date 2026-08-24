using LeaveManagement.SharedKernel.Primitives;
using SharedKernel.Shared;

namespace Domain.ValueObjects
{
    public sealed class PhoneNumber : ValueObject
    {
        public string value { get; }

        private PhoneNumber(string value) => this.value = value;

        public static ResultT<PhoneNumber> Create(string value)
        {
            if (string.IsNullOrWhiteSpace(value))
                return GeneralErrors.General.Empty(nameof(value));

            var digitsOnly = value.Trim();
            if (digitsOnly.Length < 7 || digitsOnly.Length > 15)
                return GeneralErrors.General.Invalid(nameof(value));

            return new PhoneNumber(digitsOnly);
        }

        public override IEnumerable<object> GetAtomicValues()
        {
            yield return value;
        }
    }
}
