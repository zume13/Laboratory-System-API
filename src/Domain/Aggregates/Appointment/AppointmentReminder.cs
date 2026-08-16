using Domain.Aggregates.Communications.Enums;
using LMS.SharedKernel.Primitives;
using SharedKernel.Shared;
using System;
using System.Collections.Generic;
using System.Text;

namespace Domain.Aggregates.Appointment
{
    public class AppointmentReminder : Entity
    {
        private AppointmentReminder(
            Guid id,
            Guid appointmentId,
            NotificationChannel channel,
            DateTime scheduledSendTime)
            : base(id)
        {
            AppointmentId = appointmentId;
            Channel = channel;
            ScheduledSendTime = scheduledSendTime;
            Status = NotificationStatus.Pending;
        }

        public Guid AppointmentId { get; private set; }

        public NotificationChannel Channel { get; private set; }

        public DateTime ScheduledSendTime { get; private set; }

        public NotificationStatus Status { get; private set; }

        internal static ResultT<AppointmentReminder> Create(
            Guid appointmentId,
            NotificationChannel channel,
            DateTime scheduledSendTime)
        {
            if (scheduledSendTime <= DateTime.UtcNow)
                return GeneralErrors.General.Invalid(nameof(scheduledSendTime));

            return new AppointmentReminder(Guid.NewGuid(), appointmentId, channel, scheduledSendTime);
        }

        internal void MarkSent() => Status = NotificationStatus.Sent;

        internal void MarkFailed() => Status = NotificationStatus.Failed;
    }

}
