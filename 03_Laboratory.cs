using PDDLPortal.Domain.ValueObjects;
using SharedKernel.Primitives;
using SharedKernel.Shared;

namespace PDDLPortal.Domain.Entities.Laboratory.TestCategories;


namespace PDDLPortal.Domain.Entities.Laboratory.Requests;

public enum LabRequestStatus
{
   
}


public class LabResult : Entity
{
    private LabResult(
        Guid id,
        Guid labRequestId,
        Guid uploadedByStaffId,
        PdfPath pdfPath,
        string sampleId)
        : base(id)
    {
        LabRequestId = labRequestId;
        UploadedByStaffId = uploadedByStaffId;
        PdfPath = pdfPath;
        SampleId = sampleId;
        UploadedAt = DateTime.UtcNow;
    }

    public Guid LabRequestId { get; private set; }

    public Guid UploadedByStaffId { get; private set; }

    public PdfPath PdfPath { get; private set; }

    public string SampleId { get; private set; }

    public DateTime? ReleaseDate { get; private set; }

    public bool IsVoided { get; private set; }

    public DateTime UploadedAt { get; private set; }

    internal static ResultT<LabResult> Create(
        Guid labRequestId,
        Guid uploadedByStaffId,
        PdfPath pdfPath,
        string sampleId)
    {
        if (uploadedByStaffId == Guid.Empty)
            return GeneralErrors.General.Empty(nameof(uploadedByStaffId));

        return new LabResult(Guid.NewGuid(), labRequestId, uploadedByStaffId, pdfPath, sampleId);
    }

    internal void MarkReleased() => ReleaseDate = DateTime.UtcNow;

    internal void Void() => IsVoided = true;
}
