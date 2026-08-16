using SharedKernel.Shared;

namespace Domain.ValueObjects
{
    public sealed record PdfPath
    {
        public string value { get; }

        private PdfPath(string value) => this.value = value;

        public static ResultT<PdfPath> Create(string value)
        {
            if (string.IsNullOrWhiteSpace(value))
                return GeneralErrors.General.Empty(nameof(value));

            if (!value.Trim().EndsWith(".pdf", StringComparison.OrdinalIgnoreCase))
                return GeneralErrors.General.Invalid(nameof(value));

            return new PdfPath(value.Trim());
        }
    }
}
