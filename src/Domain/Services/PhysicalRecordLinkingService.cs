using Domain.Aggregates.Identity.PatientProfile;
using Domain.Aggregates.Laboratory.LaboratoryRequest;
using SharedKernel.Shared;

namespace Domain.Services
{
    public static class PhysicalRecordLinkingDomainService
    {
        // The handler is responsible for finding matchingRequests via the repository
        // (a query, not a domain decision) and passing them in already-loaded.
        public static Result Link(
            PatientProfile profile,
            string physicalPatientId,
            IReadOnlyCollection<LaboratoryRequest> matchingRequests)
        {
            var linkResult = profile.LinkPhysicalRecord(physicalPatientId);
            if (linkResult.IsFailure)
                return linkResult.Error;

            foreach (var request in matchingRequests)
            {
                var attachResult = request.AttachPatient(profile.UserId);
                if (attachResult.IsFailure)
                    return attachResult.Error;
            }

            return Result.Success();
        }
    }
}
