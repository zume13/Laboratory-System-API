using Domain.ValueObjects;
using LMS.SharedKernel.Primitives;
using SharedKernel.Shared;

namespace Domain.Aggregates.Laboratory.LaboratoryOrder
{
    public class LaboratoryResult : Entity
    {
        private LaboratoryResult() { }
        private LaboratoryResult(
            Guid id,
            Guid laboratoryRequestId,
            Guid uploadedByStaffId,
            PdfPath pdfPath,
            string sampleId)
            : base(id)
        {
            LaboratoryRequestId = laboratoryRequestId;
            UploadedByStaffId = uploadedByStaffId;
            PdfPath = pdfPath;
            SampleId = sampleId;
            UploadedAt = DateTime.UtcNow;
        }

        public Guid LaboratoryRequestId { get; private set; }

        public Guid UploadedByStaffId { get; private set; }

        public PdfPath PdfPath { get; private set; }

        public string SampleId { get; private set; }

        public DateTime? ReleaseDate { get; private set; }

        public bool IsVoided { get; private set; }

        public DateTime UploadedAt { get; private set; }

        internal static ResultT<LaboratoryResult> Create(
            Guid laboratoryRequestId,
            Guid uploadedByStaffId,
            PdfPath pdfPath,
            string sampleId)
        {
            if (uploadedByStaffId == Guid.Empty)
                return GeneralErrors.General.Empty(nameof(uploadedByStaffId));

            if (string.IsNullOrWhiteSpace(sampleId))
                return GeneralErrors.General.Invalid(nameof(sampleId));

            return new LaboratoryResult(Guid.NewGuid(), laboratoryRequestId, uploadedByStaffId, pdfPath, sampleId);
        }

        internal Result Release()
        {
            if (IsVoided)
                return LaboratoryOrderErrors.LaboratoryResult.ResultAlreadyVoided;

            if (ReleaseDate.HasValue)
                return LaboratoryOrderErrors.LaboratoryResult.ResultAlreadyReleased;

            ReleaseDate = DateTime.UtcNow;

            return Result.Success();
        }

        internal Result Void()
        {
            if (IsVoided)
                return LaboratoryOrderErrors.LaboratoryResult.ResultAlreadyVoided;

            IsVoided = true;

            return Result.Success();
        }
    }

}
