using Domain.ValueObjects;
using SharedKernel.Primitives;
using SharedKernel.Shared;

namespace Domain.Aggregates.AppointmentSlot
{
    public class AppointmentSlot : AggregateRoot
    {
        private AppointmentSlot()
        {
        }
        private AppointmentSlot(
            Guid id,
            DateTime date,
            TimeRange timeRange,
            Guid? testCategoryId,
            int capacity,
            Guid? configuredByStaffId)
            : base(id)
        {
            Date = date;
            TimeRange = timeRange;
            TestCategoryId = testCategoryId;
            Capacity = capacity;
            BookedCount = 0;
            ConfiguredByStaffId = configuredByStaffId;
        }

        public DateTime Date { get; private set; }

        public TimeRange TimeRange { get; private set; }

        public Guid? TestCategoryId { get; private set; }

        public int Capacity { get; private set; }

        public int BookedCount { get; private set; }

        public Guid? ConfiguredByStaffId { get; private set; }

        public bool IsFull => BookedCount >= Capacity;

        public static ResultT<AppointmentSlot> Create(
            DateTime date,
            TimeRange timeRange,
            Guid? testCategoryId,
            int capacity,
            Guid? configuredByStaffId)
        {
            if (capacity <= 0)
                return GeneralErrors.General.Invalid(nameof(capacity));

            if (date.Date < DateTime.UtcNow.Date)
                return GeneralErrors.General.Invalid(nameof(date));

            return new AppointmentSlot(
                Guid.NewGuid(),
                date,
                timeRange,
                testCategoryId,
                capacity,
                configuredByStaffId);
        }

        // Internal — a slot's own booked count can only change through Reserve/Release
        // so the capacity invariant can never be bypassed by another aggregate.
        internal Result Reserve()
        {
            if (IsFull)
                return AppointmentSlotErrors.FullSlots;

            if (Date.Date < DateTime.UtcNow.Date)
                return GeneralErrors.General.Invalid(nameof(Date));

            BookedCount++;

            return Result.Success();
        }

        internal Result Release()
        {
            if (BookedCount == 0)
                return AppointmentSlotErrors.NoBookedSlots;

            BookedCount--;

            return Result.Success();
        }
    }
}
