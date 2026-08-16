using SharedKernel.Shared;

namespace Domain.ValueObjects
{
    public sealed record TimeRange
    {
        public TimeSpan Start { get; }

        public TimeSpan End { get; }

        private TimeRange(TimeSpan start, TimeSpan end)
        {
            Start = start;
            End = end;
        }

        public static ResultT<TimeRange> Create(TimeSpan start, TimeSpan end)
        {
            if (end <= start)
                return GeneralErrors.General.Invalid(nameof(end));

            return new TimeRange(start, end);
        }
    }
}
