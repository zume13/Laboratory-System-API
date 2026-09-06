using Application.Abstractions.Base;
using Application.Abstractions.FileStorage;
using Application.Abstractions.Repositories;
using Application.Features.LabOrder.Commands.UploadLaboratoryResult;
using Domain.Aggregates.Laboratory.LaboratoryOrder;
using Domain.ValueObjects;
using MediatR;
using SharedKernel.Shared;

namespace Application.Features.LabOrder.Commands.UploadLabResult
{
    public sealed class UploadLabResultCommandHandler : IRequestHandler<UploadLabResultCommand, ResultT<Guid>>
    {
        private readonly ILabOrderRepository _repository;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IFileStorageService _service;
        public UploadLabResultCommandHandler(ILabOrderRepository repository, IUnitOfWork unitOfWork, IFileStorageService service)
        {
            _repository = repository;
            _unitOfWork = unitOfWork;
            _service = service;
        }   
        public async Task<ResultT<Guid>> Handle(UploadLabResultCommand request, CancellationToken cancellationToken)
        {
            var labOrder = await _repository.GetLabOrderWithLabRequestForUpdateAsync(request.labOrderId, cancellationToken);

            if (labOrder is null)
                return LaboratoryOrderErrors.LabOrder.NotFound;

            var canUpload = labOrder.CanUploadResult(request.requestId);

            if (canUpload.IsFailure)
                return canUpload.Error;

            var uploadFile = await _service.StoreFileAsync(request.fileName, request.fileStream, request.subFolder, cancellationToken);

            if(uploadFile.IsFailure)
                return uploadFile.Error;

            var pdfPath = PdfPath.Create(uploadFile.value);

            if (pdfPath.IsFailure)
            {
                var deleteFile = await _service.DeleteFileAsync(pdfPath.value.value!, cancellationToken);

                if(deleteFile.IsFailure)
                    return deleteFile.Error;

                return pdfPath.Error;
            }

            var uploadResult = labOrder.UploadResult(request.requestId, request.uploadedByStaffId, pdfPath.value , request.sampleId);

            if (uploadResult.IsFailure) 
            {
                var deleteFile = await _service.DeleteFileAsync(pdfPath.value.value!, cancellationToken);

                if (deleteFile.IsFailure)
                    return deleteFile.Error;

                return uploadResult.Error;
            }           

            var saveResult = await _unitOfWork.SaveChangesAsync(cancellationToken);

            if (saveResult.IsFailure)
            {
                var deleteFile = await _service.DeleteFileAsync(pdfPath.value.value!, cancellationToken);

                if (deleteFile.IsFailure)
                    return deleteFile.Error;

                return saveResult.Error;
            }

            return ResultT<Guid>.Success(uploadResult.value.Id);
        }
    }
}
