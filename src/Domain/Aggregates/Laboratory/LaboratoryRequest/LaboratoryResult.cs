using Domain.ValueObjects;
using LMS.SharedKernel.Primitives;
using SharedKernel.Shared;

namespace Domain.Aggregates.Laboratory.LaboratoryRequest
{
    public class LaboratoryResult : Entity
    {
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

            return new LaboratoryResult(Guid.NewGuid(), laboratoryRequestId, uploadedByStaffId, pdfPath, sampleId);
        }

        internal void MarkReleased() => ReleaseDate = DateTime.UtcNow;

        internal void Void() => IsVoided = true;
    }

}
