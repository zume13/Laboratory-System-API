using Application.Dto;
using Domain.Aggregates.AppointmentSlot;

namespace Application.Features.AppointmentSlots
{
    public static class AppointmentSlotMapper
    {
        public static AppointmentSlotDto ToDto(AppointmentSlot slot) =>
            new(slot.Id, slot.Date, slot.TimeRange.Start, slot.TimeRange.End,
                slot.TestCategoryId, slot.Capacity, slot.BookedCount, slot.ConfiguredByStaffId);

        public static PublicAppointmentSlotDto ToPublicDto(AppointmentSlot slot) =>
            new(slot.Id, slot.Date, slot.TimeRange.Start, slot.TimeRange.End,
                slot.TestCategoryId, slot.Capacity - slot.BookedCount);
    }
}