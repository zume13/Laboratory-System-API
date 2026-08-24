using LeaveManagement.SharedKernel.Primitives;
using SharedKernel.Shared;

namespace Domain.ValueObjects
{
    public sealed class TimeRange : ValueObject
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

        public override IEnumerable<object> GetAtomicValues()
        {
            throw new NotImplementedException();
        }
    }
}
