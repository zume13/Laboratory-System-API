using Domain.Aggregates.Communications.Enums;
using Domain.Aggregates.Communications.Notification;
using Domain.Aggregates.Laboratory.LaboratoryRequest;
using SharedKernel.Shared;

namespace Domain.Services
{
    public static class LabResultReleaseDomainService
    {
        // Releasing a result and notifying the patient are one domain event, even
        // though they touch two aggregates — the handler just persists whatever
        // this returns.
        public static ResultT<Notification> Release(LaboratoryRequest labRequest)
        {
            if (labRequest.PatientId is null)
                return GeneralErrors.General.Empty(nameof(labRequest.PatientId));

            var releaseResult = labRequest.Release();
            if (releaseResult.IsFailure)
                return releaseResult.Error;

            return Notification.Dispatch(
                labRequest.PatientId.Value,
                NotificationChannel.InPortal,
                "Your lab result is now available.",
                labRequest.ActiveResult?.Id);
        }
    }
}
