using Domain.ValueObjects;
using SharedKernel.Primitives;
using SharedKernel.Shared;

namespace Domain.Aggregates.Laboratory.LaboratoryRequest
{
    public class LaboratoryRequest : AggregateRoot
    {
        private readonly List<LaboratoryResult> _results = new();

        private LaboratoryRequest() { }
        private LaboratoryRequest(
            Guid id,
            Guid? patientId,
            string? physicalPatientId,
            Guid testCategoryId,
            string clinicalDetails,
            Guid? appointmentId)
            : base(id)
        {
            PatientId = patientId;
            PhysicalPatientId = physicalPatientId;
            TestCategoryId = testCategoryId;
            ClinicalDetails = clinicalDetails;
            AppointmentId = appointmentId;
            Status = RequestStatus.Pending;
            CreatedAt = DateTime.UtcNow;
        }

        // Nullable — a walk-in request may exist before the patient has a digital account.
        // See PhysicalPatientId / AttachPatient for the linking flow.
        public Guid? PatientId { get; private set; }

        public string? PhysicalPatientId { get; private set; }

        public Guid TestCategoryId { get; private set; }

        public Guid? AppointmentId { get; private set; }

        public string ClinicalDetails { get; private set; }

        public RequestStatus Status { get; private set; }

        public DateTime CreatedAt { get; private set; }

        public IReadOnlyCollection<LaboratoryResult> Results => _results.AsReadOnly();

        public LaboratoryResult? ActiveResult => _results.FirstOrDefault(r => !r.IsVoided);

        // Used when the patient already has a digital account (online booking, or an
        // appointment being converted — see AppointmentFulfillmentService).
        public static ResultT<LaboratoryRequest> CreateForPatient(
            Guid patientId,
            Guid testCategoryId,
            string clinicalDetails,
            Guid? appointmentId = null)
        {
            if (patientId == Guid.Empty)
                return GeneralErrors.General.Empty(nameof(patientId));

            if (testCategoryId == Guid.Empty)
                return GeneralErrors.General.Empty(nameof(testCategoryId));

            return new LaboratoryRequest(
                Guid.NewGuid(),
                patientId,
                physicalPatientId: null,
                testCategoryId,
                clinicalDetails ?? string.Empty,
                appointmentId);
        }

        // Used by clinic staff for a walk-in with no digital account yet — see
        // PhysicalRecordLinkingService for how this gets attached to a Patient later.
        public static ResultT<LaboratoryRequest> CreateForWalkIn(
            string physicalPatientId,
            Guid testCategoryId,
            string clinicalDetails)
        {
            if (string.IsNullOrWhiteSpace(physicalPatientId))
                return GeneralErrors.General.Empty(nameof(physicalPatientId));

            if (testCategoryId == Guid.Empty)
                return GeneralErrors.General.Empty(nameof(testCategoryId));

            return new LaboratoryRequest(
                Guid.NewGuid(),
                patientId: null,
                physicalPatientId,
                testCategoryId,
                clinicalDetails ?? string.Empty,
                appointmentId: null);
        }

        // Called once the walk-in patient registers and links their receipt number.
        public Result AttachPatient(Guid patientId)
        {
            if (patientId == Guid.Empty)
                return GeneralErrors.General.Empty(nameof(patientId));

            if (PatientId is not null)
                return LaboratoryRequestErrors.LaboratoryRequest.ExistingPatientId;

            PatientId = patientId;

            return Result.Success();
        }

        public ResultT<LaboratoryResult> AttachResult(
            Guid uploadedByStaffId,
            string pdfFilePath,
            string sampleId)
        {
            if (Status == RequestStatus.Voided)
                return LaboratoryRequestErrors.LaboratoryRequest.VoidedRequest;

            if (ActiveResult is not null)
                return LaboratoryRequestErrors.LaboratoryRequest.ResultAlreadyAttached;

            var pdfPath = PdfPath.Create(pdfFilePath);

            if (pdfPath.IsFailure)
                return pdfPath.Error;

            var result = LaboratoryResult.Create(Id, uploadedByStaffId, pdfPath.value, sampleId);

            if (result.IsFailure)
                return result.Error;

            _results.Add(result.value);

            return result.value;
        }

        public Result Release()
        {
            if (ActiveResult is null)
                return GeneralErrors.General.Empty(nameof(ActiveResult));

            if (Status == RequestStatus.Released)
                return LaboratoryRequestErrors.LaboratoryRequest.RequestResultAlreadyReleased;

            Status = RequestStatus.Released;
            ActiveResult.MarkReleased();

            return Result.Success();
        }

        public Result Void()
        {
            if (Status == RequestStatus.Released)
                return LaboratoryRequestErrors.LaboratoryRequest.RequestResultAlreadyReleased;

            Status = RequestStatus.Voided;
            ActiveResult?.Void();

            return Result.Success();
        }
    }

}
