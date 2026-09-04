using Domain.Aggregates.Laboratory.LaboratoryOrder.Enums;
using SharedKernel.Primitives;
using SharedKernel.Shared;

namespace Domain.Aggregates.Laboratory.LaboratoryOrder
{ 
    public class LaboratoryRequestOrder : AggregateRoot 
    { 
        private readonly List<LaboratoryRequest> _requests = new(); 
        private LaboratoryRequestOrder() { } 
        private LaboratoryRequestOrder(Guid id, Guid patientId, Guid appointmentId) : base(id) 
        { 
            PatientId = patientId; 
            AppointmentId = appointmentId; 
            Status = LabOrderStatus.InProgress; 
            CreatedAt = DateTime.UtcNow; 
        } 
            public Guid PatientId { get; private set; } 
            public Guid AppointmentId { get; private set; } 
            public LabOrderStatus Status { get; private set; } 
            public DateTime CreatedAt { get; private set; } 
            public DateTime? CompletedAt { get; private set; } 

            public IReadOnlyCollection<LaboratoryRequest> Requests => _requests.AsReadOnly(); 

            public static ResultT<LaboratoryRequestOrder> Create(Guid patientId, Guid appointmentId) 
            { 
                if (patientId == Guid.Empty) 
                    return GeneralErrors.General.Empty(nameof(patientId)); 

                if (appointmentId == Guid.Empty) 
                    return GeneralErrors.General.Empty(nameof(appointmentId)); 
            
                return new LaboratoryRequestOrder(Guid.NewGuid(), patientId, appointmentId); 
            } 

        /// <summary> /// Creates a laboratory request as part of this order. /// 
        /// LabRequest is an entity owned by this aggregate. /// 
        /// </summary> 
        public ResultT<LaboratoryRequest> AddRequest( Guid testCategoryId) 
        { 
            if (Status is LabOrderStatus.Completed or LabOrderStatus.Cancelled) 
                return LaboratoryOrderErrors.Request.InvalidStatus; 

            if (testCategoryId == Guid.Empty) 
                return GeneralErrors.General.Empty(nameof(testCategoryId)); 
            
            if (_requests.Any(r => r.TestCategoryId == testCategoryId && r.Status != RequestStatus.Voided)) 
                return LaboratoryOrderErrors.Request.DuplicateRequest; 
            
            var request = LaboratoryRequest.Create(Id, PatientId, testCategoryId); 
            
            if (request.IsFailure) 
                return request.Error; 
            
            _requests.Add(request.value);

            return request.value;
        }

        public ResultT<LaboratoryRequest> RemoveRequest(Guid testCategoryId)
        {
            if (Status is LabOrderStatus.Completed or LabOrderStatus.Cancelled)
                return LaboratoryOrderErrors.Request.InvalidStatus;

            if (testCategoryId == Guid.Empty)
                return GeneralErrors.General.Empty(nameof(testCategoryId));

            if (_requests.Any(r => r.TestCategoryId == testCategoryId && r.Status != RequestStatus.Voided))
                return LaboratoryOrderErrors.Request.DuplicateRequest;

            var request = LaboratoryRequest.Create(Id, PatientId, testCategoryId);

            if (request.IsFailure)
                return request.Error;

            _requests.Remove(request.value);

            return request.value;
        }

        /// <summary> /// Completes the order only when all active requests /// have been completed. /// </summary> 
        public Result Complete() 
        { 
            if (Status is LabOrderStatus.Completed or LabOrderStatus.Cancelled) 
                return LaboratoryOrderErrors.Request.InvalidStatus; 

            if (_requests.Count == 0) 
                return LaboratoryOrderErrors.Request.NoRequestsProvided; 

            if (_requests.Any(r => r.Status == RequestStatus.Pending)) 
                return LaboratoryOrderErrors.Request.RequestsStillPending;

            Status = LabOrderStatus.Completed; 
            CompletedAt = DateTime.UtcNow; 

            return Result.Success(); 
        } 
        public Result Cancel() 
        { 
            if (Status is LabOrderStatus.Completed or LabOrderStatus.Cancelled) 
                return LaboratoryOrderErrors.Request.InvalidStatus; 
            
            foreach (var request in _requests .Where(r => r.Status != RequestStatus.Released && r.Status != RequestStatus.Voided)) 
            { 
                request.Void(); 
            } 
            
            Status = LabOrderStatus.Cancelled;
            
            return Result.Success(); 
        } 
    } 
}