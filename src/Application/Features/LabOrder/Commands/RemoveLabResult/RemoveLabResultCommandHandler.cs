using Application.Abstractions.Base;
using Application.Abstractions.FileStorage;
using Application.Abstractions.Repositories;
using Domain.Aggregates.Laboratory.LaboratoryOrder;
using MediatR;
using SharedKernel.Shared;

namespace Application.Features.LabOrder.Commands.RemoveLabResult
{
    public class RemoveLabResultCommandHandler : IRequestHandler<RemoveLabResultCommand, Result>
    {
        private readonly IFileStorageService _service;
        private readonly ILabOrderRepository _repository;
        private readonly IUnitOfWork _unitOfWork;

        public RemoveLabResultCommandHandler(IFileStorageService service, ILabOrderRepository repository, IUnitOfWork unitOfWork)
        {
            _service = service;
            _repository = repository;
            _unitOfWork = unitOfWork;
        }
        public async Task<Result> Handle(RemoveLabResultCommand request, CancellationToken cancellationToken) 
        { 
            var labOrder = await _repository.GetLabOrderWithLabRequestForUpdateAsync(request.labOrderId, cancellationToken); 

                if (labOrder is null) 
                    return LaboratoryOrderErrors.LabOrder.NotFound; 

            var labRequest = labOrder.Requests.FirstOrDefault(r => r.Id == request.requestId);
            
                if (labRequest is null) 
                    return LaboratoryOrderErrors.LaboratoryResult.NotFound(request.requestId); 
            
            var filePath = labRequest.labResult?.PdfPath.value; 
            
            var removeResult = labOrder.RemoveResult(request.requestId); 
            
                if (removeResult.IsFailure) 
                    return removeResult.Error; 
            
                if (!string.IsNullOrWhiteSpace(filePath)) 
                { 
                    var deleteResult = _service.DeleteFile(filePath); 
                
                    if (deleteResult.IsFailure) 
                        return deleteResult.Error; 
                } 
            
            var saveResult = await _unitOfWork.SaveChangesAsync(cancellationToken); 
            
            if (saveResult.IsFailure) 
                return saveResult.Error; 
            
            return Result.Success(); }
    }
}
