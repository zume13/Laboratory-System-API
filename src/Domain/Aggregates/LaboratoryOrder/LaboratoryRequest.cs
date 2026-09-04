using Domain.Aggregates.LaboratoryOrder;
using Domain.Aggregates.LaboratoryOrder.Enums;
using Domain.ValueObjects;
using LMS.SharedKernel.Primitives;
using SharedKernel.Shared;

namespace Domain.Aggregates.Laboratory 
{ 
    public class LaboratoryRequest : Entity 
    {
        private LaboratoryResult? _result; 
        private LaboratoryRequest() { } 
        private LaboratoryRequest(Guid id, Guid labOrderId, Guid patientId, Guid testCategoryId) : base(id) 
        { 
            LabOrderId = labOrderId; 
            PatientId = patientId; 
            TestCategoryId = testCategoryId; 
            Status = RequestStatus.Pending; 
            CreatedAt = DateTime.UtcNow; } 
        public Guid LabOrderId { get; private set; } 
        public Guid PatientId { get; private set; } 
        public Guid TestCategoryId { get; private set; }
        public RequestStatus Status { get; private set; } 
        public DateTime CreatedAt { get; private set; } 
        public DateTime? CompletedAt { get; private set; } 
        public LaboratoryResult? labResult => _result; 

        internal static ResultT<LaboratoryRequest> Create(Guid labOrderId, Guid patientId, Guid testCategoryId) 
        { 
            if (labOrderId == Guid.Empty) 
                return GeneralErrors.General.Empty(nameof(labOrderId)); 

            if (patientId == Guid.Empty) 
                return GeneralErrors.General.Empty(nameof(patientId)); 

            if (testCategoryId == Guid.Empty) 
                return GeneralErrors.General.Empty(nameof(testCategoryId)); 
            
            return new LaboratoryRequest(Guid.NewGuid(), labOrderId, patientId, testCategoryId); 
        } 
        internal ResultT<LaboratoryResult> UploadResult(Guid uploadedByStaffId, PdfPath pdfPath, string sampleId) 
        { 
            if (Status != RequestStatus.Pending) 
                return LaboratoryOrderErrors.Request.InvalidStatus; 

            if (_result is not null) 
                return LaboratoryOrderErrors.Request.RequestAlreadyExitsts; 

            var result = LaboratoryResult.Create(Id, uploadedByStaffId, pdfPath, sampleId); 

            if (result.IsFailure) return result.Error; _result = result.value; 
            return result.value; 
        } 
        internal Result Release() 
        { 
            if (Status != RequestStatus.Pending) 
                return LaboratoryOrderErrors.Request.InvalidStatus; 

            if (_result is null)
                return LaboratoryOrderErrors.Request.NotFound(Id);
            
            Status = RequestStatus.Released; 

            return Result.Success(); 
        } 
        internal Result Complete() 
        { 
            if (Status != RequestStatus.Pending) 
                return LaboratoryOrderErrors.Request.InvalidStatus; 

            if (_result is null) 
                return LaboratoryOrderErrors.Request.NotFound(Id); 

            Status = RequestStatus.Completed; 
            CompletedAt = DateTime.UtcNow; 

            return Result.Success(); 
        } 
        internal Result Void() 
        { 
            if (Status is RequestStatus.Completed or RequestStatus.Voided) 
                return LaboratoryOrderErrors.Request.InvalidStatus; 

            if (_result is not null) 
            { 
                var result = _result.Void(); 
                if (result.IsFailure) 
                    return result.Error; 
            } 

            Status = RequestStatus.Voided; 

            return Result.Success(); 
        } 
    } 
}