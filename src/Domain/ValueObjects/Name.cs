using SharedKernel.Shared;

namespace PDDLPortal.Domain.ValueObjects
{

    public sealed record Name
    {
        public string value { get; }

        private Name(string value) => this.value = value;

        public static ResultT<Name> Create(string value)
        {
            if (string.IsNullOrWhiteSpace(value))
                return GeneralErrors.General.Empty(nameof(value));

            if (value.Trim().Length > 100)
                return GeneralErrors.General.Invalid(nameof(value));

            return new Name(value.Trim());
        }
    }
}