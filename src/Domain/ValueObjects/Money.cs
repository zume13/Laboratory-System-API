using LeaveManagement.SharedKernel.Primitives;
using SharedKernel.Shared;

namespace Domain.ValueObjects
{
    public sealed class Money : ValueObject
    {
        public decimal value { get; }

        private Money(decimal value) => this.value = value;

        public static ResultT<Money> Create(decimal value)
        {
            if (value < 0)
                return GeneralErrors.General.Invalid(nameof(value));

            return new Money(value);
        }

        public override IEnumerable<object> GetAtomicValues()
        {
            yield return value;
        }
    }

}
