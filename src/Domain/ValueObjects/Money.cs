using SharedKernel.Shared;

namespace Domain.ValueObjects
{
    public sealed record Money
    {
        public decimal value { get; }

        private Money(decimal value) => this.value = value;

        public static ResultT<Money> Create(decimal value)
        {
            if (value < 0)
                return GeneralErrors.General.Invalid(nameof(value));

            return new Money(value);
        }
    }

}
